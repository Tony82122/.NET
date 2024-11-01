using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using ApiContracts.DTOs;

namespace BlazorApp1.HttpServices;

public class HttpPostService : IPostService
{
    private readonly HttpClient _client;

    public HttpPostService(HttpClient client)
    {
        _client = client;
    }

    public async Task<PostDTO> AddPostAsync(int userId, PostDTO post)
    {
        var response = await _client.PostAsJsonAsync($"api/users/{userId}/posts", post);
        response.EnsureSuccessStatusCode();
        return await _client.GetFromJsonAsync<PostDTO>(
            $"api/users/{userId}/posts/{response.Headers.Location.Segments.Last()}") ?? throw new InvalidOperationException();
    }

    public async Task UpdatePostAsync(int userId, int postId, PostDTO post)
    {
        var response = await _client.PutAsJsonAsync($"api/users/{userId}/posts/{postId}", post);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeletePostAsync(int userId, int postId)
    {
        var response = await _client.DeleteAsync($"api/users/{userId}/posts/{postId}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<List<PostDTO>> GetPostsAsync()
    {
        var posts = await _client.GetFromJsonAsync<List<PostDTO>>("api/posts");
        return posts ?? new List<PostDTO>();
    }
}