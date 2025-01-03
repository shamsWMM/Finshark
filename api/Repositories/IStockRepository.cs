using api.Dtos;
using api.Helpers;
using api.Models;

namespace api.Repositories;
public interface IStockRepository
{
    Task<IEnumerable<Stock>> GetStocks(QueryObject query);
    Task<Stock?> GetStock(int id);
    Task<int> CreateStock(StockDto stockDto);
    Task<int?> UpdateStock(int id, StockDto stockDto);
    Task<int?> DeleteStock(int id);
    Task<bool> StockExists(int id);
}