using Entities;

namespace EntityRepository;

public interface IUserRepo
{
    Task<int> AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
    Task<User> GetSingleAsync(int id);
    
    IQueryable<User> GetAll();
    Task<IQueryable<User>> GetManyAsync();
    //Task<UserDTO> GetUserByUsernameAsync(string username);
    Task <User>GetUserByUsernameAsync(string requestUserName);
}