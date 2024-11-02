using ApiContracts;
using System.Threading.Tasks;
using ApiContracts.DTOs;


namespace BlazorApp1.HttpServices;

public interface IPostService
{
    Task<List<postDTO>> GetPostsAsync();
    Task<postDTO> AddPostAsync(int userId, postDTO post);
    Task UpdatePostAsync(int userId, int postId, postDTO post);
    Task DeletePostAsync(int userId, int postId);
    Task<List<postDTO>> GetRecentPostsAsync(int userId, DateTime startDate, DateTime endDate);    
    Task<List <postDTO>> GetPopularPostsAsync(int userId); 
    Task<List<postDTO>> GetTopPostsAsync(int userId, DateTime startDate, DateTime endDate);
    
    
    
}