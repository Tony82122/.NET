using Entities;
using EntityRepository;

namespace EfcRepositories;

public class EfcPostRepository : IPostRepo
{
    public Task<Post> AddAsync(Post post)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Post post)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Post> GetSingleAsync(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Post> GetAll()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Post> GetManyAsync()
    {
        throw new NotImplementedException();
    }
}