using api.Data;
using api.Dtos;
using api.Mappers;
using api.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;

public class PortfolioRepository(ApplicationDBContext context) : IPortfolioRepository
{
    public async Task<IEnumerable<StockDto>> GetPortfolio(string userId)
    {
        return await context.Portfolio.Where(p => p.UserId.Equals(userId))
            .Include(p => p.Stock)
            .ThenInclude(s => s.Comments)
            .Select(p => p.Stock.ToDto())
            .ToListAsync();
    }

    public async Task AddStock(string userId, int stockId)
    {
        var portfolio = new Portfolio
        {
            UserId = userId,
            StockId = stockId
        };
        await context.Portfolio.AddAsync(portfolio);
        await context.SaveChangesAsync();
    }
}