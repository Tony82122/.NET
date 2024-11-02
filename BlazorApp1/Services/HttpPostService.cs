using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using ApiContracts.DTOs;
using Microsoft.Extensions.Logging;

namespace BlazorApp1.HttpServices;

public class HttpPostService : IPostService
{
    private readonly HttpClient _client;
    private readonly ILogger<HttpPostService> _logger;

    public HttpPostService(HttpClient client, ILogger<HttpPostService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<postDTO> AddPostAsync(int userId, postDTO post)
    {
        try
        {
            _logger.LogInformation($"Adding new post for user {userId}");
            var response = await _client.PostAsJsonAsync($"api/users/{userId}/posts", post);
            response.EnsureSuccessStatusCode();
            var newPost = await response.Content.ReadFromJsonAsync<postDTO>();
            _logger.LogInformation($"Successfully added new post with ID {newPost?.Id}");
            return newPost ?? throw new InvalidOperationException("Failed to deserialize the new post");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while adding post for user {userId}");
            throw;
        }
    }

    public async Task UpdatePostAsync(int userId, int postId, postDTO post)
    {
        try
        {
            _logger.LogInformation($"Updating post {postId} for user {userId}");
            var response = await _client.PutAsJsonAsync($"api/users/{userId}/posts/{postId}", post);
            response.EnsureSuccessStatusCode();
            _logger.LogInformation($"Successfully updated post {postId}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while updating post {postId} for user {userId}");
            throw;
        }
    }

    public async Task DeletePostAsync(int userId, int postId)
    {
        try
        {
            _logger.LogInformation($"Deleting post {postId} for user {userId}");
            var response = await _client.DeleteAsync($"api/users/{userId}/posts/{postId}");
            response.EnsureSuccessStatusCode();
            _logger.LogInformation($"Successfully deleted post {postId}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while deleting post {postId} for user {userId}");
            throw;
        }
    }

    public async Task<List<postDTO>> GetRecentPostsAsync(int userId, DateTime startDate, DateTime endDate)
    {
        try
        {
            _logger.LogInformation($"Fetching recent posts for user {userId} from {startDate} to {endDate}");
            var posts = await _client.GetFromJsonAsync<List<postDTO>>(
                $"api/users/{userId}/posts?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
            _logger.LogInformation($"Successfully fetched {posts?.Count ?? 0} recent posts");
            return posts ?? new List<postDTO>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while fetching recent posts for user {userId}");
            throw;
        }
    }

    public async Task<List<postDTO>> GetPopularPostsAsync(int userId)
    {
        return await GetPopularPostsAsync(userId, 10, 5);
    }

    public async Task<List<postDTO>> GetTopPostsAsync(int userId, DateTime startDate, DateTime endDate)
    {
        try
        {
            _logger.LogInformation($"Fetching top posts for user {userId} from {startDate} to {endDate}");
            var posts = await _client.GetFromJsonAsync<List<postDTO>>(
                $"api/users/{userId}/posts/top?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
            _logger.LogInformation($"Successfully fetched {posts?.Count ?? 0} top posts");
            return posts ?? new List<postDTO>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while fetching top posts for user {userId}");
            throw;
        }
    }

    public async Task<List<postDTO>> GetPopularPostsAsync(int userId, int upVotes, int downVotes)
    {
        try
        {
            _logger.LogInformation($"Fetching popular posts for user {userId} with upvotes >= {upVotes} and downvotes <= {downVotes}");
            var posts = await _client.GetFromJsonAsync<List<postDTO>>($"api/users/{userId}/posts/popular?upvotes={upVotes}&downvotes={downVotes}");
            _logger.LogInformation($"Successfully fetched {posts?.Count ?? 0} popular posts");
            return posts ?? new List<postDTO>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while fetching popular posts for user {userId}");
            throw;
        }
    }

    public async Task<List<postDTO>> GetPostsAsync()
    {
        try
        {
            _logger.LogInformation("Fetching all posts");
            var posts = await _client.GetFromJsonAsync<List<postDTO>>("api/posts");
            _logger.LogInformation($"Successfully fetched {posts?.Count ?? 0} posts");
            return posts ?? new List<postDTO>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching all posts");
            throw;
        }
    }
}