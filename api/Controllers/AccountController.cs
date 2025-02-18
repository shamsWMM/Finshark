using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static api.Helpers.ValidationHelper;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(UserManager<ApplicationUser> userManager, ITokenService tokenService, SignInManager<ApplicationUser> signInManager) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserDto userDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new ApplicationUser
            {
                UserName = userDto.Username,
                Email = userDto.Email
            };

            var result = await userManager.CreateAsync(user, userDto.Password);
            if (result.Succeeded)
            {
                var roleResult = await userManager.AddToRoleAsync(user, "User");
                if (roleResult.Succeeded)
                    return Ok(UserItem(userDto.Username, userDto.Email, tokenService.GenerateToken(user)));
                else
                    return StatusCode(500, roleResult.Errors);
            }
            return StatusCode(500, result.Errors);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginDto userDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var user = await userManager.Users.FirstOrDefaultAsync(x => x.UserName.Equals(userDto.Username));
        if (user == null)
            return Unauthorized(ItemNotFound(Item.User, userDto.Username));

        var result = await signInManager.CheckPasswordSignInAsync(user, userDto.Password, false);

        if (result.Succeeded)
            return Ok(UserItem(user.UserName, user.Email, tokenService.GenerateToken(user)));
        else
            return Unauthorized(InvalidLogin);
    }
}
