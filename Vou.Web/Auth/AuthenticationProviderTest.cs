using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Vou.Web.Auth
{
    public class AuthenticationProviderTest : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await Task.Delay(3000);   //Para Realizar pruebas en el arranque
            var anonimous = new ClaimsIdentity();
            var HebalmertUser = new ClaimsIdentity(new List<Claim>
                {
                    new Claim("FirstName", "Hebert"),
                    new Claim("LastName", "Merchan"),
                    new Claim(ClaimTypes.Name, "nexxtplanet.net@gmail.com"),
                    new Claim(ClaimTypes.Role, "Admin")
                },
                authenticationType: "Test");
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(HebalmertUser)));
        }
    }
}
