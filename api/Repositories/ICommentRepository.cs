using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Models;

namespace api.Repositories;
public interface ICommentRepository
{
    Task<IEnumerable<Comment>> GetComments();
    Task<Comment?> GetComment(int id);
    Task<int> CreateComment(string userId, int stockId, CommentDto commentDto);
    Task<int?> UpdateComment(int id, CommentDto commentDto);
    Task<int?> DeleteComment(int id);

}