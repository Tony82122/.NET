using ApiContracts;
using System.Threading.Tasks;
using ApiContracts.DTOs;


namespace BlazorApp1.HttpServices;

public interface IPostService
{
    Task<List<PostDTO>> GetPostsAsync();
    
}