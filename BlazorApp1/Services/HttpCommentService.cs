using System;
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
        _client.BaseAddress = new Uri("https://localhost:5001/");
    }

    public async Task<List<CommentDTO>> GetCommentsByPostIdAsync(int postId)
    {
        var response = await _client.GetAsync($"api/comments?postId={postId}");
        await EnsureSuccessStatusCodeAsync(response, $"retrieve comments for post ID {postId}");
        return await response.Content.ReadFromJsonAsync<List<CommentDTO>>() ?? new List<CommentDTO>();
    }

    public async Task<CommentDTO> AddCommentAsync(int postId, CommentDTO comment)
    {
        var response = await _client.PostAsJsonAsync($"api/comments?postId={postId}", comment);
        await EnsureSuccessStatusCodeAsync(response, $"add comment to post ID {postId}");
        return await response.Content.ReadFromJsonAsync<CommentDTO>() ?? throw new InvalidOperationException("Failed to deserialize the response");
    }

    public async Task UpdateCommentAsync(int commentId, CommentDTO comment)
    {
        var response = await _client.PutAsJsonAsync($"api/comments/{commentId}", comment);
        await EnsureSuccessStatusCodeAsync(response, $"update comment ID {commentId}");
    }

    public async Task DeleteCommentAsync(int commentId)
    {
        var response = await _client.DeleteAsync($"api/comments/{commentId}");
        await EnsureSuccessStatusCodeAsync(response, $"delete comment ID {commentId}");
    }

    private async Task EnsureSuccessStatusCodeAsync(HttpResponseMessage response, string operation)
    {
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Failed to {operation}. Status code: {response.StatusCode}, Content: {content}");
        }
    }
}