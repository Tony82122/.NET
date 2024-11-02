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

    public async Task<postDTO> AddPostAsync(int userId, postDTO post)
    {
        var response = await _client.PostAsJsonAsync($"api/users/{userId}/posts", post);
        response.EnsureSuccessStatusCode();
        return await _client.GetFromJsonAsync<postDTO>(
            $"api/users/{userId}/posts/{response.Headers.Location.Segments.Last()}") ?? throw new InvalidOperationException();
    }

    public async Task UpdatePostAsync(int userId, int postId, postDTO post)
    {
        var response = await _client.PutAsJsonAsync($"api/users/{userId}/posts/{postId}", post);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeletePostAsync(int userId, int postId)
    {
        var response = await _client.DeleteAsync($"api/users/{userId}/posts/{postId}");
        response.EnsureSuccessStatusCode();
    }

    Task<List<postDTO>> IPostService.GetRecentPostsAsync(int userId, DateTime startDate,DateTime endDate)
    {
        return GetRecentPostsAsync(userId);
    }

    public Task<List<postDTO>> GetPopularPostsAsync(int userId)
    {
        return GetPopularPostsAsync(userId, 10, 5);
        
    }

    public  async Task<List<postDTO>> GetTopPostsAsync(int userId, DateTime startDate, DateTime endDate)
    {
        var posts = await _client.GetFromJsonAsync<List<postDTO>>(
            $"api/users/{userId}/posts?startdate={startDate.ToString("yyyy-MM-dd")}&enddate   ");   
        return posts?? new List<postDTO>();
    }

    public async Task <List<postDTO>> GetPopularPostsAsync(int userId, int upVotes, int downVotes) 
    {
        var posts = await _client.GetFromJsonAsync<List<postDTO>>($"api/users/{userId}/posts?upvotes={upVotes}&downvotes={downVotes}");
        return posts?? new List<postDTO>();
    }

    public async Task<List<postDTO>> GetPostsAsync()
    {
        var posts = await _client.GetFromJsonAsync<List<postDTO>>("api/posts");
        return posts ?? new List<postDTO>();
    }

    public static async Task<List<postDTO>> GetRecentPostsAsync(int userId)
    {
        return new List<postDTO>()
        { 
            new postDTO { Title = "The game last night", Body = "I dont like the way England play so terrible in tournaments", UserId = "1267" },
        new postDTO() { Title = "AFTV", Body = "He has to go blud!", UserId = "1075" },
        new postDTO() { Title = "Football talk", Body = "When will Arsenal win the Champions league??", UserId = "2234" },
        new postDTO() { Title = "Election results", Body = "How many votes did Boris get??", UserId = "1568" },
        new postDTO() { Title = "Climate change", Body = "We need to act now!!", UserId = "3321" },
        new postDTO() { Title = "Coronavirus", Body = "It's a pandemic!!", UserId = "4456" },
        new postDTO() { Title = "Music trends", Body = "Why is Taylor Swift a thing??", UserId = "5567" }
            
        };
        await Task.Delay(1000); 
    }

   
    
    
}