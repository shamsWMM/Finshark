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

    [HttpGet("{id:int}")]
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
        var stock = stockDto.ToStock();
        _context.Stock.Add(stock);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetStock), new { id = stock.Id }, stock.ToDto());
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateStock([FromRoute] int id, [FromBody] StockDto stockDto)
    {
        var stock = _context.Stock.Find(id);
        if (stock == null)
        {
            return NotFound();
        }

        stock.Symbol = stockDto.Symbol;
        stock.CompanyName = stockDto.CompanyName;
        stock.Purchase = stockDto.Purchase;
        stock.LastDiv = stockDto.LastDiv;
        stock.Industry = stockDto.Industry;
        stock.MarketCap = stockDto.MarketCap;

        _context.SaveChanges();
        return Ok(stock.ToDto());
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteStock([FromRoute] int id)
    {
        var stock = _context.Stock.Find(id);
        if (stock == null)
        {
            return NotFound();
        }

        _context.Stock.Remove(stock);
        _context.SaveChanges();
        return NoContent();
    }
}
