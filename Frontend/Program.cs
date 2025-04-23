using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Microsoft.AspNetCore.Components.Authorization;

using Blazored.LocalStorage; 

using Frontend;
using Frontend.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args); 

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();


builder.Services.AddAuthorizationCore(); 
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>(); 
builder.Services.AddScoped<CustomAuthenticationStateProvider>(); 


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7153/") });


builder.Services.AddScoped<TokenHandler>(); 

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BackendApiClient"));

builder.Services.AddHttpClient("BackendApiClient", client =>
{
 
    var apiBaseAddress = builder.Configuration["BackendApi:Url"];
    if (string.IsNullOrEmpty(apiBaseAddress))
    {
        
        apiBaseAddress = builder.HostEnvironment.BaseAddress; 

        Console.WriteLine("WARNING: URL of BackendApi:Url is not set");

        throw new InvalidOperationException("BackendApi:Url not set. Cannot configure HttpClient.");
    }
    client.BaseAddress = new Uri(apiBaseAddress);

})
.AddHttpMessageHandler<TokenHandler>(); 

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7153/") });

await builder.Build().RunAsync();