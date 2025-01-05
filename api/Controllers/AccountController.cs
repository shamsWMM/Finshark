using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static api.Helpers.ValidationHelper;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(UserManager<ApplicationUser> userManager) : ControllerBase
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
                    return Ok(ItemCreated(Item.User, userDto.Username));
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
}
