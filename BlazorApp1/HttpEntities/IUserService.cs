using System.Threading.Tasks;
using ApiContracts.DTOs;

namespace BlazorApp1.HttpServices
{
    public interface IUserService
    {
        Task<UserDto> AddUserAsync(UserDto user);
        Task<UserDto> GetUserAsync(int id);
        Task UpdateUserAsync(int id, UserDto user);
        Task DeleteUserAsync(int id);
        Task<UserDto> LoginAsync(string username, string password);
        Task<UserDto> RegisterAsync(string username, string password);
        Task<bool> CheckUsernameAvailabilityAsync(string username);
        Task<bool> CheckEmailAvailabilityAsync(string email);
        Task<UserDto> GetByEmailAsync(string email);
        Task<UserDto> GetByUsernameAsync(string username);
        Task <UserDto> GetBySubscribedUsersAsync(int userId);
        Task<UserDto> GetByJoined(int userId, int subscribedUserId);
        
    }
}