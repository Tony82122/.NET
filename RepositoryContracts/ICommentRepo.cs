﻿using Entities;

namespace EntityRepository;

public interface ICommentRepository
{
    Task<Comment> AddAsync(Comment comment);
    Task UpdateAsync(Comment comment);
    Task DeleteAsync(int id);
    Task<Comment> GetSingleAsync(int id);
    
    IQueryable<Comment> GetMany();
    IQueryable<Comment> GetAll();
    IQueryable<Comment> GetManyAsync(int userId);
}