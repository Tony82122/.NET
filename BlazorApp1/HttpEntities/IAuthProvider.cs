using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorApp1.HttpEntities
{
    public interface IAuthProvider
    {
        Task LoginAsync(string userName, string password);
        Task<AuthenticationState> GetAuthenticationStateAsync();
        Task<AuthenticationState> GetAuthorizationStateAsync();
        Task Logout();
    }
}