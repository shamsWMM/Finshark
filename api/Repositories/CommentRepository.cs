using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;
public class CommentRepository(ApplicationDBContext context) : ICommentRepository
{
    public async Task<IEnumerable<Comment>> GetComments()
        => await context.Comment
            .AsNoTracking()
            .ToListAsync();

    public async Task<Comment?> GetComment(int id)
        => await context.Comment
            .FindAsync(id);

    public async Task<int> CreateComment(int stockId, CommentDto commentDto)
    {
        var comment = commentDto.ToComment();
        comment.StockId = stockId;
        
        await context.Comment.AddAsync(comment);
        await context.SaveChangesAsync();
        return comment.Id;
    }

    public async Task<int?> UpdateComment(int id, CommentDto commentDto)
    {
        var comment = await context.Comment.FindAsync(id);
        if (comment == null)
            return null;

        commentDto.ToComment(comment);
        await context.SaveChangesAsync();
        return comment.Id;
    }

    public async Task<int?> DeleteComment(int id)
    {
        var comment = await context.Comment.FindAsync(id);
        if (comment == null)
            return null;

        context.Comment.Remove(comment);
        await context.SaveChangesAsync();
        return comment.Id;
    }
}