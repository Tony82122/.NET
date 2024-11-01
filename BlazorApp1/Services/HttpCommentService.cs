using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using ApiContracts.DTOs;

namespace BlazorApp1.HttpServices;

public class HttpCommentService : ICommentService
{
    private readonly HttpClient _client;

    public HttpCommentService(HttpClient client)
    {
        _client = client;
    }

    public async Task<CommentDTO> AddCommentAsync(int postId, CommentDTO comment)
    {
        var response = await _client.PostAsJsonAsync($"api/posts/{postId}/comments", comment);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<CommentDTO>() ??
               throw new InvalidOperationException("Failed to retrieve the created comment.");
    }

    public async Task<CommentDTO> GetCommentAsync(int postId, int commentId)
    {
        return await _client.GetFromJsonAsync<CommentDTO>($"api/posts/{postId}/comments/{commentId}") ??
               throw new InvalidOperationException("Failed to retrieve the comment.");
    }

    public async Task UpdateCommentAsync(int postId, int commentId, CommentDTO request)
    {
        var response = await _client.PutAsJsonAsync($"api/posts/{postId}/comments/{commentId}", request);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteCommentAsync(int postId, int commentId)
    {
        var response = await _client.DeleteAsync($"api/posts/{postId}/comments/{commentId}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<List<CommentDTO>> GetCommentsByPostIdAsync(int postId)
    {
        var comments = await _client.GetFromJsonAsync<List<CommentDTO>>($"api/posts/{postId}/comments");
        return comments ?? new List<CommentDTO>();
    }
}