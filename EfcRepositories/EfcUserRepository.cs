using Entities;
using EntityRepository;

namespace EfcRepositories;

public class EfcUserRepository : IUserRepo

    
{
    private readonly AppContext _context;
    
    public EfcUserRepository(AppContext context)
    {
        _context = context;
    }
    public Task<int> AddAsync(User user)
    {
        _context.Users.Add(user);
        return _context.SaveChangesAsync();
        
 
        
    }

    public Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        return _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var userToDelete = await _context.Users.FindAsync(id);
        if (userToDelete!= null)
        {
            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<User> GetSingleAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public IQueryable<User> GetAll()
    {
        return _context.Users;
    }

    public Task<IQueryable<User>> GetManyAsync()
    {
        return Task.FromResult(_context.Users.AsQueryable());
    }

    public Task<User> GetUserByUsernameAsync(string requestUserName)
    {
        throw new NotImplementedException();
    }
}