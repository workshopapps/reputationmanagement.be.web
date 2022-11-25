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
            var query = user.Select(x => new CustomerAccountForCreationDto()
            {
                BusinessEntityName = x.UserName,
                Email = x.Email,
                Password = x.PasswordHash,
                
            }).ToList();
            return Ok(query);
        }
        return BadRequest();

    }

    [SwaggerOperation(Summary = "Create a Review with this endpoint")]
    [HttpPost("create")]
    [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
    public ActionResult CreateReview([FromBody] ReviewForCreationDto reviewForCreationDto)
    {
        // use this to get user Id From request and 
        var review = _reviewRepo.CreateReviews(reviewForCreationDto);
        return Ok(review);
    }

    [SwaggerOperation(Summary = "Get all reviews for Admin")]
    [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
    [HttpGet("reviews")]
    public ActionResult GetAllReviews(int pageNumber = 0, int pageSize = 10)
    {
        var reviews = _reviewRepo.GetAllReviews(pageNumber, pageSize).ToList();
       var reviewsToReturn = _mapper.Map<IEnumerable<ReviewForDisplayDto>>(reviews);
        return Ok(reviewsToReturn);

    }


    [SwaggerOperation(Summary = "Get a particular reviews for Admin")]
    [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
    [HttpGet("reviews/{reviewId}")]
     public IActionResult GetSingleReview(Guid reviewId)
    {
        if (reviewId == Guid.Empty)
        {
            return NotFound(reviewId);
        }
        Review singleReview = _reviewRepo.GetReviewById(reviewId);

        if (singleReview == null)
            return NotFound();

        return Ok(singleReview);
    }

    [SwaggerOperation(Summary = "Returns all inconclusive reviews (statusType = inconclusive)")]
    [HttpGet("inconclusiveReviews")]
    [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
    public IActionResult GetAllInconclusiveReviews()
    {
        var inconclusiveReviews = _reviewRepo.GetInconclusiveReviews();
        return Ok(inconclusiveReviews);
    }

    [HttpGet]
    [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
    [Route("SuccessfulReview")]
    public async Task<IActionResult> SuccessReview()
    {
        var resultModel = new List<SuccessfulReviewsDto>();
        var query = await _reviewRepo.GetAllSuccessfulReview();
        return Ok(query);
    }

    [HttpGet]
    [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
    [Route("Reveiws/Priority")]
    public IActionResult GetreviewByPriority(PriorityType priority)
    {
       // var resultModel = new List<SuccessfulReviewsDto>();
        var query = _reviewRepo.GetReviewByPropirity(priority);
        if (query == null)
        {
            return NotFound();
        }
        return Ok(query);
    }

    [SwaggerOperation(Summary = "Update a review by an Admin")]
    [HttpPut]
    [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
    [Route("{reviewId}/update")]
    public ActionResult UpdateReview([FromBody]  ReviewForUpdateDTO review)
    {
       
    var reviews =  _reviewRepo.UpdateReviewLawyer( review);
        if (review == null)
        {
            return NotFound();
        }
        return Ok("Review is successfully updated");
    }

    [SwaggerOperation(Summary = "delete a review by an Admin")]
    [HttpDelete]
    [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
    [Route("{reviewId}/delete")]
    public ActionResult DeleteReview (Guid reviewId)
    {

        _reviewRepo.DeleteReview(reviewId);
        _reviewRepo.Save();
        return Ok("Review is successfully deleted");
    }
}

