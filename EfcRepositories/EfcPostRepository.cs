using Entities;
using EntityRepository;

namespace EfcRepositories;

public class EfcPostRepository : IPostRepo{
    private readonly AppContext _context;
    
    public EfcPostRepository(AppContext context)
    {
        _context = context;
    }



    public Task<int> AddAsync(Post post)
    {
        _context.Posts.Add(post);
        return _context.SaveChangesAsync();
        
    }

    public async Task UpdateAsync(Post post)
    {
        _context.Posts.Update(post);
        _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post!= null)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Post> GetSingleAsync(int id)
    {
        return await _context.Posts.FindAsync(id) ?? throw new ArgumentException("Post not found", nameof(id));
    }

    public IQueryable<Post> GetAll()
    {
        return _context.Posts;
    }

    public IEnumerable<Post> GetManyAsync()
    {
        return _context.Posts.ToList();
    }
}