using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Models;

namespace api.Mappers;
public static class CommentMapper
{
    public static CommentDto ToDto(this Comment comment)
    {
        var commentDto = new CommentDto();
        commentDto.Id = comment.Id;
        commentDto.Title = comment.Title;
        commentDto.Content = comment.Content;

        return commentDto;
    }

    public static Comment ToComment(this CommentDto commentDto, Comment? comment = null)
    {
        comment ??= new Comment();
        comment.Title = commentDto.Title;
        comment.Content = commentDto.Content;

        return comment;
    }
}