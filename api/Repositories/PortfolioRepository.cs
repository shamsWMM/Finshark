using api.Data;
using api.Dtos;
using api.Mappers;
using api.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;

public class PortfolioRepository(ApplicationDBContext context) : IPortfolioRepository
{
    public async Task<IEnumerable<StockDto>> GetPortfolio(ApplicationUser user)
    {
        return await context.Portfolio.Where(p => p.UserId.Equals(user.Id))
            .Select(p => p.Stock.ToDto())
            .ToListAsync();
    }
}