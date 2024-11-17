using Entities;
using EntityRepository;

namespace EfcRepositories;

public class EfcUserRepository : IUserRepo
    
{
    public Task<User> AddAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetSingleAsync(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<User> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<User>> GetManyAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserByUsernameAsync(string requestUserName)
    {
        throw new NotImplementedException();
    }
}