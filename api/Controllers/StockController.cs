using api.Data;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using api.Dtos;
using api.Models;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockController(ApplicationDBContext context) : ControllerBase
{
    private readonly ApplicationDBContext _context = context;

    [HttpGet]
    public IActionResult GetStocks()
    {
        var stocks = _context.Stock.ToList().Select(s => s.ToDto());
        return Ok(stocks);
    }

    [HttpGet("{id}")]
    public IActionResult GetStock([FromRoute] int id)
    {
        var stock = _context.Stock.Find(id);
        return stock switch
        {
            null => NotFound(),
            _ => Ok(stock.ToDto())
        };
    }

    [HttpPost]
    public IActionResult CreateStock([FromBody] StockDto stockDto)
    {
        Stock stock = stockDto.ToStock();
        _context.Stock.Add(stock);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetStock), new { id = stock.Id }, stock.ToDto());
    }
}
