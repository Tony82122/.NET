using Entities;
using EntityRepository;

namespace EfcRepositories;

public class EfcCommentRepository : ICommentRepository
{
    private readonly AppContext _context;
    public EfcCommentRepository(AppContext context)
    {
        _context = context;
    }
    public async Task<Comment> AddAsync(Comment comment)
    {
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
        return  comment;
        
    }

    public Task UpdateAsync(Comment comment)
    {
        _context.Comments.Update(comment);
        return _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var commentToDelete = await _context.Comments.FindAsync(id);
        if (commentToDelete != null)
        {
            _context.Comments.Remove(commentToDelete);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Comment> GetSingleAsync(int id)
    { 
        return await _context.Comments.FindAsync(id);
        
    
    }

    public IQueryable<Comment> GetMany()
    {
        return _context.Comments;
    }

    public IQueryable<Comment> GetAll()
    {
        return _context.Comments;
    }

    public IQueryable<Comment> GetManyAsync(int userId)
    {
return _context.Comments.Where(c => c.UserId == userId);    }
}