using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using ApiContracts.DTOs;

namespace BlazorApp1.HttpServices;

public class HttpUserService : IUserService
{
    private readonly HttpClient _client;

    public HttpUserService(HttpClient client)
    {
        _client = client;
    }

    public async Task<UserDto> AddUserAsync(UserDto request)
    {
        var response = await _client.PostAsJsonAsync("users", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<UserDto>() ??
               throw new InvalidOperationException("Failed to deserialize user data");
    }

    public async Task<UserDto> GetUserAsync(int id)
    {
        return await _client.GetFromJsonAsync<UserDto>($"users/{id}") ??
               throw new InvalidOperationException($"User with id {id} not found");
    }

    public async Task UpdateUserAsync(int id, UserDto request)
    {
        var response = await _client.PutAsJsonAsync($"users/{id}", request);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteUserAsync(int id)
    {
        var response = await _client.DeleteAsync($"users/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<UserDto> LoginAsync(string username, string password)
    {
        var loginRequest = new { Username = username, Password = password };
        var response = await _client.PostAsJsonAsync("users/login", loginRequest);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<UserDto>() ??
               throw new InvalidOperationException("Failed to deserialize user data");
    }

    public async Task<UserDto> RegisterAsync(string username, string password)
    {
        var registerRequest = new { Username = username, Password = password };
        var response = await _client.PostAsJsonAsync("users/register", registerRequest);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<UserDto>() ??
               throw new InvalidOperationException("Failed to deserialize user data");
    }

    public async Task<bool> CheckUsernameAvailabilityAsync(string username)
    {
        var response = await _client.GetAsync($"users/check-username?username={Uri.EscapeDataString(username)}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<bool>();
    }

    public async Task<bool> CheckEmailAvailabilityAsync(string email)
    {
        var response = await _client.GetAsync($"users/check-email?email={Uri.EscapeDataString(email)}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<bool>();
    }

    public async Task<UserDto> GetByEmailAsync(string email)
    {
        var response = await _client.GetAsync($"users/by-email?email={Uri.EscapeDataString(email)}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<UserDto>() ??
               throw new InvalidOperationException($"User with email {email} not found");
    }

    public async Task<UserDto> GetByUsernameAsync(string username)
    {
        var response = await _client.GetAsync($"users/by-username?username={Uri.EscapeDataString(username)}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<UserDto>() ??
               throw new InvalidOperationException($"User with username {username} not found");
    }

    public async Task<UserDto> GetBySubscribedUsersAsync(int userId)
    {
        var response = await _client.GetAsync($"users/{userId}/subscribed");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<UserDto>() ??
               throw new InvalidOperationException($"Failed to retrieve subscribed users for user {userId}");
    }

    public async Task<UserDto> GetByJoined(int userId, int subscribedUserId)
    {
        var response = await _client.GetAsync($"users/{userId}/joined/{subscribedUserId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<UserDto>() ??
               throw new InvalidOperationException();
    }
}