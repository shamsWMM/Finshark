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
        var portfolio = await portfolioRepository.GetPortfolio(user);
        return Ok(portfolio);
    }
}
