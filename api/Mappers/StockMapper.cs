using api.Models;
using api.Dtos;

namespace api.Mappers;
public static class StockMapper
{
    public static StockDto ToDto(this Stock stock)
    {
        var stockDto = new StockDto();
        stockDto.Id = stock.Id; 
        stockDto.Symbol = stock.Symbol;
        stockDto.CompanyName = stock.CompanyName;
        stockDto.Purchase = stock.Purchase;
        stockDto.LastDiv = stock.LastDiv;
        stockDto.Industry = stock.Industry;
        stockDto.MarketCap = stock.MarketCap;
        stockDto.Comments = stock.Comments.Select(c => c.ToDto());

        return stockDto;
    }

    public static Stock ToStock(this StockDto stockDto, Stock? stock = null)
    {
        stock ??= new Stock();
        stock.Symbol = stockDto.Symbol;
        stock.CompanyName = stockDto.CompanyName;
        stock.Purchase = stockDto.Purchase;
        stock.LastDiv = stockDto.LastDiv;
        stock.Industry = stockDto.Industry;
        stock.MarketCap = stockDto.MarketCap;

        return stock;
    }
}
