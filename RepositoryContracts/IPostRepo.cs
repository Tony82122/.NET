
using Entities;

namespace EntityRepository;

public interface IPostRepo
{
    Task<int> AddAsync(Post post);
    Task UpdateAsync(Post post);
    Task DeleteAsync(int id);
    Task<Post> GetSingleAsync(int id);
    
    
    IQueryable<Post> GetAll();
    IEnumerable<Post> GetManyAsync();
    
}