using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Models;

namespace api.Repositories;

public interface IPortfolioRepository
{
    Task<IEnumerable<StockDto>> GetPortfolios(string userId);
    Task CreatePortfolio(string userId, int stockId);
    Task RemovePortfolio(string userId, int stockId);
}
