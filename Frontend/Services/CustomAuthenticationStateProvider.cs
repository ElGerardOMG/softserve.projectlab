// Por ejemplo, en una carpeta "Services" o "Authentication" en tu proyecto Blazor
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage; // Si usas Blazored.LocalStorage
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt; // Para JwtSecurityTokenHandler

namespace Frontend.Services; // O tu namespace

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService;
    private readonly HttpClient _httpClient; // Necesario si usas HttpClient para validar el token con API o si necesitas hacer logout via API

    public CustomAuthenticationStateProvider(ILocalStorageService localStorageService, HttpClient httpClient)
    {
        _localStorageService = localStorageService;
        _httpClient = httpClient; // Puedes necesitar HttpClient para futuras validaciones o logout
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // 1. Intentar obtener el token del almacenamiento local
        var token = await _localStorageService.GetItemAsStringAsync("authToken"); // Usa la misma clave con la que guardaste el token

        if (string.IsNullOrEmpty(token))
        {
            // Si no hay token, el usuario no está autenticado
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        // 2. Validar y leer el token (parsear Claims)
        var claimsPrincipal = CreateClaimsPrincipalFromToken(token);

        // 3. Devolver el estado de autenticación
        return new AuthenticationState(claimsPrincipal);
    }

    // Método para crear ClaimsPrincipal desde el token JWT
    private ClaimsPrincipal CreateClaimsPrincipalFromToken(string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken == null)
            {
                // Token invalido o no es JWT
                return new ClaimsPrincipal(new ClaimsIdentity());
            }

            // Opcional: Validar la fecha de expiracion del token en el cliente
            if (jsonToken.ValidTo < DateTime.UtcNow)
            {
                // Token expirado, limpiar almacenamiento
                _localStorageService.RemoveItemAsync("authToken").ConfigureAwait(false);
                return new ClaimsPrincipal(new ClaimsIdentity());
            }


            // Obtener los claims del token
            var claims = jsonToken.Claims;

            // Crear la identidad y el principal
            var userIdentity = new ClaimsIdentity(claims, "jwtAuthType"); // "jwtAuthType" es un nombre de autenticacion
            var claimsPrincipal = new ClaimsPrincipal(userIdentity);

            return claimsPrincipal;
        }
        catch
        {
            // Error al procesar el token, considerarlo invalido
            _localStorageService.RemoveItemAsync("authToken").ConfigureAwait(false); // Limpiar token invalido
            return new ClaimsPrincipal(new ClaimsIdentity());
        }
    }


    // Método para llamar después de un Login exitoso
    public async Task MarkUserAsAuthenticated(string token)
    {
        await _localStorageService.SetItemAsStringAsync("authToken", token); // Guarda el token

        var claimsPrincipal = CreateClaimsPrincipalFromToken(token);

        // Notifica a Blazor que el estado de autenticación ha cambiado
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }

    // Método para llamar después de un Logout
    public async Task MarkUserAsLoggedOut()
    {
        await _localStorageService.RemoveItemAsync("authToken"); // Elimina el token

        // Crea un principal vacío (usuario no autenticado)
        var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());

        // Notifica a Blazor que el estado de autenticación ha cambiado
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));
    }
}