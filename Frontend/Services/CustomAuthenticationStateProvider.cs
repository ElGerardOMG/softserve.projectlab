
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage; 
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt; 

namespace Frontend.Services; 

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService;
    private readonly HttpClient _httpClient; 

    public CustomAuthenticationStateProvider(ILocalStorageService localStorageService, HttpClient httpClient)
    {
        _localStorageService = localStorageService;
        _httpClient = httpClient; 
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        
        var token = await _localStorageService.GetItemAsStringAsync("authToken"); 

        if (string.IsNullOrEmpty(token))
        {
            
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        
        var claimsPrincipal = CreateClaimsPrincipalFromToken(token);

        
        return new AuthenticationState(claimsPrincipal);
    }

    
    private ClaimsPrincipal CreateClaimsPrincipalFromToken(string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken == null)
            {
                
                return new ClaimsPrincipal(new ClaimsIdentity());
            }

            
            if (jsonToken.ValidTo < DateTime.UtcNow)
            {
                
                _localStorageService.RemoveItemAsync("authToken").ConfigureAwait(false);
                return new ClaimsPrincipal(new ClaimsIdentity());
            }

           
            var claims = jsonToken.Claims;

            
            var userIdentity = new ClaimsIdentity(claims, "jwtAuthType"); 
            var claimsPrincipal = new ClaimsPrincipal(userIdentity);

            return claimsPrincipal;
        }
        catch
        {
            
            _localStorageService.RemoveItemAsync("authToken").ConfigureAwait(false); 
            return new ClaimsPrincipal(new ClaimsIdentity());
        }
    }


    public async Task MarkUserAsAuthenticated(string token)
    {
        await _localStorageService.SetItemAsStringAsync("authToken", token); 

        var claimsPrincipal = CreateClaimsPrincipalFromToken(token);


        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }


    public async Task MarkUserAsLoggedOut()
    {
        await _localStorageService.RemoveItemAsync("authToken");


        var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());

   
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));
    }
}