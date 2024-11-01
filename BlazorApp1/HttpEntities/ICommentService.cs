using ApiContracts;
using System.Threading.Tasks;
using ApiContracts.DTOs;


namespace BlazorApp1.HttpServices;


public interface ICommentService
{
    Task<List<CommentDTO>> GetCommentsByPostIdAsync(int id);
    
    
}