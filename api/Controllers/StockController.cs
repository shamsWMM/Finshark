using api.Data;
using Microsoft.AspNetCore.Mvc;
using api.Dtos;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;
using api.Repositories;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockController(IStockRepository stockRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetStocks()
    {
        var stocks = await stockRepository.GetStocks();
        return Ok(stocks.Select(s => s.ToDto()));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetStock([FromRoute] int id)
    {
        var stock = await stockRepository.GetStock(id);
        return stock switch
        {
            null => StockNotfFound(id),
            _ => Ok(stock.ToDto())
        };
    }

    [HttpPost]
    public async Task<IActionResult> CreateStock([FromBody] StockDto stockDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var stockId = await stockRepository.CreateStock(stockDto);
        var stock = await stockRepository.GetStock(stockId);
        return CreatedAtAction(nameof(GetStock), new { id = stockId }, stock?.ToDto());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] StockDto stockDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var stockId = await stockRepository.UpdateStock(id, stockDto);
        if (stockId == null)
            return StockNotfFound(id);

        var stock = await stockRepository.GetStock(stockId.Value);
        return Ok(stock?.ToDto());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteStock([FromRoute] int id)
    {
        var stock = await stockRepository.DeleteStock(id);
        if (stock == null)
            return StockNotfFound(id);

        return NoContent();
    }

    private NotFoundObjectResult StockNotfFound(int id)
    {
        object message = new { Message = $"Stock with ID {id} was not found." };
        return NotFound(message);
    }
}
