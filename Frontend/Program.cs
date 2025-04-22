using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Microsoft.AspNetCore.Components.Authorization;

using Microsoft.Extensions.Configuration; // Para leer configuracion si usas Aspire
using Microsoft.Extensions.DependencyInjection; // Para las extensiones Add...
using Microsoft.Extensions.Http; // <-- ¡Este es el paquete que agregaste!
using System.Net.Http; // Este ya lo tienes

using System.Security.Claims;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt; // Para JwtSecurityTokenHandler

using Blazored.LocalStorage; // Si usas Blazored.LocalStorage

using Frontend;
using Frontend.Services;



// ... otros usings ...

var builder = WebAssemblyHostBuilder.CreateDefault(args); // O Host.CreateDefaultBuilder(args) para Blazor Server
// ... otras configuraciones del builder ...
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Registra Blazored.LocalStorage
builder.Services.AddBlazoredLocalStorage();

// Registra tu AuthenticationStateProvider personalizado
// Debes crear la clase CustomAuthenticationStateProvider en el siguiente paso
builder.Services.AddAuthorizationCore(); // Servicios base de autorización
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>(); // <-- Tu implementacion
builder.Services.AddScoped<CustomAuthenticationStateProvider>(); // Tambien registralo directamente

// Registra tu servicio de autenticación (AuthService)

// Configura HttpClient para que use una base address (la URL de tu API)
// y adjunte el token automáticamente
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7153/") });


// --- Configuración para adjuntar el Token automáticamente ---
// Instala Microsoft.AspNetCore.Components.WebAssembly.Authentication
builder.Services.AddScoped<TokenHandler>(); // <-- Implementa TokenHandler en el siguiente paso

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BackendApiClient"));

builder.Services.AddHttpClient("BackendApiClient", client =>
{
    // Configura la base address de tu API aquí
    // Para Aspire, obtén la URL de configuracion:
    var apiBaseAddress = builder.Configuration["BackendApi:Url"]; // Obtén la URL de tu API desde appsettings.json
    if (string.IsNullOrEmpty(apiBaseAddress))
    {
        // Fallback o manejar error si la URL no está configurada
        apiBaseAddress = builder.HostEnvironment.BaseAddress; // O alguna URL por defecto

        // Manejar el caso si la URL no está configurada (ej: lanzar error, usar URL por defecto)
        // Considera obtenerla de una fuente segura en produccion
        Console.WriteLine("Advertencia: La URL de BackendApi:Url no está configurada.");
        // Fallback solo para desarrollo si no usas Aspire o appsettings correctamente:
        // client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
        throw new InvalidOperationException("BackendApi:Url no está configurada. No se puede configurar HttpClient.");
    }
    client.BaseAddress = new Uri(apiBaseAddress);

})
.AddHttpMessageHandler<TokenHandler>(); // Usa tu TokenHandler para adjuntar el token




//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7153/") });

await builder.Build().RunAsync();