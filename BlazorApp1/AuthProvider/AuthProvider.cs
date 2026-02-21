using System.Security.Claims;
using System.Text.Json;
using ApiContracts.DTOs;
using BlazorApp1.HttpEntities;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Shared.ApiContracts;

namespace BlazorApp1.Auth;

public class AuthProvider : AuthenticationStateProvider, IAuthProvider
{
    private readonly HttpClient httpClient;
    private readonly IJSRuntime jsRuntime;
    private const string DummyUsername = "Hello";
    private const string DummyPassword = "Moto";
    private const string StorageKey = "currentUser";

    private AuthenticationState? _cachedAuthState;

    public AuthProvider(HttpClient httpClient, IJSRuntime jsRuntime)
    {
        this.httpClient = httpClient;
        this.jsRuntime = jsRuntime;
    }

    public async Task LoginAsync(string userName, string password)
    {
        if (userName == DummyUsername && password == DummyPassword)
        {
            var userDto = new UserDto
            {
                Id = 1,
                UserName = DummyUsername
            };

            string serialisedData = JsonSerializer.Serialize(userDto);
            await jsRuntime.InvokeVoidAsync("localStorage.setItem", StorageKey, serialisedData);
            
            _cachedAuthState = await CreateAuthenticationStateAsync(userDto);
            NotifyAuthenticationStateChanged(Task.FromResult(_cachedAuthState));
        }
        else
        {
            throw new Exception("Invalid username or password");
        }
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (_cachedAuthState != null)
        {
            return _cachedAuthState;
        }

        try
        {
            var userAsJson = await jsRuntime.InvokeAsync<string>("localStorage.getItem", StorageKey);

            if (string.IsNullOrEmpty(userAsJson))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var userDto = JsonSerializer.Deserialize<UserDto>(userAsJson);
            if (userDto == null)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            _cachedAuthState = await CreateAuthenticationStateAsync(userDto);
            return _cachedAuthState;
        }
        catch (InvalidOperationException)
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
    }

    private Task<AuthenticationState> CreateAuthenticationStateAsync(UserDto userDto)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userDto.UserName ?? string.Empty),
            new Claim(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
        };

        var identity = new ClaimsIdentity(claims, "apiauth");
        var user = new ClaimsPrincipal(identity);

        return Task.FromResult(new AuthenticationState(user));
    }

    public Task<AuthenticationState> GetAuthorizationStateAsync()
    {
        return GetAuthenticationStateAsync();
    }

    public async Task Logout()
    {
        await jsRuntime.InvokeVoidAsync("localStorage.removeItem", StorageKey);
        _cachedAuthState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        NotifyAuthenticationStateChanged(Task.FromResult(_cachedAuthState));
    }
}