// Por ejemplo, en una carpeta "Services" o "Handlers" en tu proyecto Blazor
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Blazored.LocalStorage; // Si usas Blazored.LocalStorage

namespace Frontend.Services;
public class TokenHandler : DelegatingHandler
{
    private readonly ILocalStorageService _localStorageService;

    public TokenHandler(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Intenta obtener el token antes de cada peticion
        var token = await _localStorageService.GetItemAsStringAsync("authToken");

        if (!string.IsNullOrEmpty(token))
        {
            // Si hay un token, adjuntalo al encabezado Authorization
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        // Continua con la peticion HTTP
        return await base.SendAsync(request, cancellationToken);
    }
}

