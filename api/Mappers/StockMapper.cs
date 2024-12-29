using api.Models;
using api.Dtos.Stock;

namespace api.Mappers;
public static class StockMapper
{
    public static StockDto ToDto(this Stock stock)
    {
        return new StockDto
        {
            Id = stock.Id,
            Symbol = stock.Symbol,
            CompanyName = stock.CompanyName,
            Purchase = stock.Purchase,
            LastDiv = stock.LastDiv,
            Industry = stock.Industry,
            MarketCap = stock.MarketCap
        };
    }
}
