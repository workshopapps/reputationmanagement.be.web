using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using src.Entities;
using src.Models.Dtos;
using src.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace src.Controllers;

[Route("api/Admin")]
[ApiController]
[Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
public class AdminController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    private readonly IReviewRepository _reviewRepo;
    private readonly IMapper _mapper;


    public AdminController(UserManager<ApplicationUser> userManager, IReviewRepository reviewRepository, IMapper mapper)
    {
        _userManager = userManager;
        _reviewRepo = reviewRepository;
        _mapper = mapper;
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
    [SwaggerOperation(Summary = "Create a Review with this endpoint")]
    [HttpPost("admin/create")]
    public ActionResult CreateReview([FromBody] ReviewForCreationDto reviewForCreationDto)
    {
        // use this to get user Id From request and 
 
    }
}