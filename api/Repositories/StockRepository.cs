using System.Runtime.CompilerServices;
using api.Data;
using api.Dtos;
using api.Helpers;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;
public class StockRepository(ApplicationDBContext context) : IStockRepository
{
    public async Task<IEnumerable<Stock>> GetStocks(QueryObject query)
    {
        var stocks = context.Stock.AsNoTracking()
            .AsQueryable()
            .ApplyFilters(query)
            .ApplyOrderBy(query)
            .Include(s => s.Comments);

        var totalCount = await stocks.CountAsync();
        var pagedStocks = stocks.ApplyPaging(query, totalCount);

        return await pagedStocks.ToListAsync();
    }

    public async Task<Stock?> GetStock(int id)
        => await context.Stock
            .AsNoTracking()
            .Include(s => s.Comments)
            .FirstOrDefaultAsync(s => s.Id == id);

    public async Task<int> CreateStock(StockDto stockDto)
    {
        var stock = stockDto.ToStock();
        await context.Stock.AddAsync(stock);
        await context.SaveChangesAsync();
        return stock.Id;
    }

    public async Task<int?> UpdateStock(int id, StockDto stockDto)
    {
        var stock = await context.Stock.FindAsync(id);
        if (stock == null)
            return null;

        stockDto.ToStock(stock);
        await context.SaveChangesAsync();
        return stock.Id;
    }

    public async Task<int?> DeleteStock(int id)
    {
        var stock = await context.Stock.FindAsync(id);
        if (stock == null)
            return null;

        context.Stock.Remove(stock);
        await context.SaveChangesAsync();
        return stock.Id;
    }

    public async Task<bool> StockExists(int id)
        => await context.Stock.AnyAsync(s => s.Id == id);

}