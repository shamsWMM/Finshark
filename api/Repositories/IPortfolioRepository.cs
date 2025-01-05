using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Models;

namespace api.Repositories;

public interface IPortfolioRepository
{
    Task<IEnumerable<StockDto>> GetPortfolio(string userId);
    Task AddStock(string userId, int stockId);
}
