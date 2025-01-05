using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static api.Helpers.ClaimsExtensions;
using static api.Helpers.ValidationHelper;

namespace api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PortfolioController(UserManager<ApplicationUser> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepository) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetPorfolio()
    {
        var Username = User.GetUsername();
        var user = await userManager.FindByNameAsync(Username);
        var portfolio = await portfolioRepository.GetPortfolios(user.Id);
        return Ok(portfolio);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddPortfolio(int stockId)
    {
        var Username = User.GetUsername();
        var user = await userManager.FindByNameAsync(Username);
        if(!await stockRepository.StockExists(stockId))
            return BadRequest(ItemNotFound(Item.Stock, stockId));

        var portfolio = await portfolioRepository.GetPortfolios(user.Id);
        if(portfolio.Any(stockDto => stockDto.Id == stockId))
            return BadRequest(ItemExists(Item.Stock, stockId));

        await portfolioRepository.CreatePortfolio(user.Id, stockId);
        portfolio = await portfolioRepository.GetPortfolios(user.Id);
        if(!portfolio.Any(stockDto => stockDto.Id == stockId))
            return StatusCode(500, FailedToCreate);
        
        return Created();
    }

    [HttpDelete("{stockId}")]
    [Authorize]
    public async Task<IActionResult> RemovePortfolio(int stockId)
    {
        var Username = User.GetUsername();
        var user = await userManager.FindByNameAsync(Username);
        var portfolio = await portfolioRepository.GetPortfolios(user.Id);
        if(!portfolio.Any(stockDto => stockDto.Id == stockId))
            return BadRequest(ItemNotFound(Item.Stock, stockId));

        await portfolioRepository.RemovePortfolio(user.Id, stockId);
        portfolio = await portfolioRepository.GetPortfolios(user.Id);
        if(portfolio.Any(stockDto => stockDto.Id == stockId))
            return StatusCode(500, FailedToDelete);
        
        return NoContent();
    }

}
