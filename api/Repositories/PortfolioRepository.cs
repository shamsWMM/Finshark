using api.Data;
using api.Dtos;
using api.Mappers;
using api.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;

public class PortfolioRepository(ApplicationDBContext context) : IPortfolioRepository
{
    public async Task<IEnumerable<StockDto>> GetPortfolios(string userId)
    {
        return await context.Portfolio.Where(p => p.UserId.Equals(userId))
            .Include(p => p.Stock)
            .ThenInclude(s => s.Comments)
            .ThenInclude(c => c.User)
            .Select(p => p.Stock.ToDto())
            .ToListAsync();
    }

    public async Task CreatePortfolio(string userId, int stockId)
    {
        var portfolio = new Portfolio
        {
            UserId = userId,
            StockId = stockId
        };
        await context.Portfolio.AddAsync(portfolio);
        await context.SaveChangesAsync();
    }

    public async Task RemovePortfolio(string userId, int stockId)
    {
        var portfolio = await context.Portfolio.FirstOrDefaultAsync(p => p.UserId.Equals(userId) && p.StockId == stockId);
        if (portfolio != null)
        {
            context.Portfolio.Remove(portfolio);
            await context.SaveChangesAsync();
        }
    }
}