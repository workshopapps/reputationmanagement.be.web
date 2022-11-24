using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using src.Entities;
using src.Models.Dtos;

namespace src.Controllers;

[Route("api/Admin")]
[ApiController]
[Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
public class AdminController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpDelete("DeleteUser")]
    public async Task<IActionResult> DeleteUser(string Id)
    {
        var user = await _userManager.FindByIdAsync(Id);

        if(user != null)
        {
            var result = await _userManager.DeleteAsync(user);
            if(result.Succeeded)
            {
                return Ok("User Deleted");
            }
        }

        return BadRequest();

    }


    [HttpPut("UpdateUserAccount")]
    public async Task<IActionResult> UpdateUser(CustomerAccountForCreationDto userDetails)
    {
        var user = await _userManager.FindByEmailAsync(userDetails.Email);
        if (user != null) {   
            IdentityResult result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok("updated");
            }
        }
        return Ok(user);
    }

    [HttpGet("GetUsers")]
    public IActionResult GetUsers()
    {
        var user = _userManager.Users;
        if (user != null)
        {
            return Ok(user);
        }
        return BadRequest();

    }
}

//_mapper.Map<IEnumerable<ReviewForDisplayDto>>(reviews);