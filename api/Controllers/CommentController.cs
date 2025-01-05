using Microsoft.AspNetCore.Mvc;
using api.Repositories;
using api.Mappers;
using api.Dtos;
using static api.Helpers.ValidationHelper;
using Microsoft.AspNetCore.Identity;
using api.Models;
using api.Helpers;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController(ICommentRepository commentRepository, IStockRepository stockRepository, UserManager<ApplicationUser> userManager) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetComments()
    {
        var comments = await commentRepository.GetComments();
        return Ok(comments.Select(c => c.ToDto()));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetComment([FromRoute] int id)
    {
        var comment = await commentRepository.GetComment(id);
        return comment switch
        {
            null => NotFound(ItemNotFound(Item.Comment, id)),
            _ => Ok(comment.ToDto())
        };
    }

    [HttpPost("{stockId:int}")]
    public async Task<IActionResult> CreateComment(int stockId, [FromBody] CommentDto commentDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        if (!await stockRepository.StockExists(stockId))
            return BadRequest(ItemNotFound(Item.Stock, stockId));

        var username = User.GetUsername();
        var user = await userManager.FindByNameAsync(username);

        var commentId = await commentRepository.CreateComment(user.Id, stockId, commentDto);
        var comment = await commentRepository.GetComment(commentId);
        return CreatedAtAction(nameof(GetComment), new { id = commentId }, comment?.ToDto());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] CommentDto commentDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var commentId = await commentRepository.UpdateComment(id, commentDto);
        if (commentId == null)
            return NotFound(ItemNotFound(Item.Comment, id));

        var comment = await commentRepository.GetComment(commentId.Value);
        return Ok(comment?.ToDto());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteComment([FromRoute] int id)
    {
        var commentId = await commentRepository.DeleteComment(id);
        return commentId == null
            ? NotFound(ItemNotFound(Item.Comment, id))
            : Ok(ItemDeleted(Item.Comment, id));
    }
}