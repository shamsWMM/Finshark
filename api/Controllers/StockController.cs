using api.Data;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using api.Dtos;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockController(ApplicationDBContext context) : ControllerBase
{
    private readonly ApplicationDBContext _context = context;

    [HttpGet]
    public async Task<IActionResult> GetStocks()
    {
        var stocks = await _context.Stock
            .AsNoTracking()
            .ToListAsync();
        var stocksDto = stocks.Select(s => s.ToDto());
        return Ok(stocksDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetStock([FromRoute] int id)
    {
        var stock = await _context.Stock.FindAsync(id);
        return stock switch
        {
            null => NotFound(),
            _ => Ok(stock.ToDto())
        };
    }

    [HttpPost]
    public async Task<IActionResult> CreateStock([FromBody] StockDto stockDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var stock = stockDto.ToStock();
        await _context.Stock.AddAsync(stock);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetStock), new { id = stock.Id }, stock.ToDto());

    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] StockDto stockDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var stock = await _context.Stock.FindAsync(id);
        if (stock == null)
            return NotFound(new { Message = $"Stock with ID {id} not found." });

        stockDto.ToStock(stock);
        await _context.SaveChangesAsync();
        return Ok(stock.ToDto());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteStock([FromRoute] int id)
    {
        var stock = await _context.Stock.FindAsync(id);
        if (stock == null)
        {
            return NotFound();
        }

        _context.Stock.Remove(stock);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
