using ApiContracts;
using System.Threading.Tasks;
using ApiContracts.DTOs;


namespace BlazorApp1.HttpServices;

public interface IPostService
{
    Task<List<PostDTO>> GetPostsAsync();
    Task<PostDTO> AddPostAsync(int userId, PostDTO post);
    Task UpdatePostAsync(int userId, int postId, PostDTO post);
    Task DeletePostAsync(int userId, int postId);
    Task<List<PostDTO>> GetRecentPostsAsync(int userId);    
    Task<List <PostDTO>> GetPopularPostsAsync(int userId); 
    
    
}