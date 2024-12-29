using api.Data;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockController : ControllerBase
{
    private readonly ApplicationDBContext _context;
    public StockController(ApplicationDBContext context)
    {
        _context = context;
    }

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
}
