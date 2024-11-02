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

    public static async Task<List<PostDTO>> GetRecentPostsAsync(int userId)
    {
        return new List<PostDTO>()
        { 
            new PostDTO { Title = "The game last night", Body = "I didnt like the way England play so terrible in tournaments", UserId = "1267" },
        new PostDTO() { Title = "AFTV", Body = "He has to go blud!", UserId = "1075" },
        new PostDTO() { Title = "Football talk", Body = "When will Arsenal win the Champions league??", UserId = "2234" },
        new PostDTO() { Title = "Election results", Body = "How many votes did Boris get??", UserId = "1568" },
        new PostDTO() { Title = "Climate change", Body = "We need to act now!!", UserId = "3321" },
        new PostDTO() { Title = "Coronavirus", Body = "It's a pandemic!!", UserId = "4456" },
        new PostDTO() { Title = "Music trends", Body = "Why is Taylor Swift a thing??", UserId = "5567" }
            
        };
    }
    
    
}