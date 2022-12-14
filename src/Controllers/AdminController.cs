using AutoMapper;
using CsvHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using src.Entities;
using src.Models.Dtos;
using src.Models.ExampleModels;
using src.Services;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Globalization;

namespace src.Controllers;

[Route("api/Admin")]
[ApiController]
[Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
public class AdminController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    private readonly IReviewRepository _reviewRepo;
    private readonly IMapper _mapper;
    private readonly IQuoteRepository _quoteRepo;

    public AdminController(UserManager<ApplicationUser> userManager, IReviewRepository reviewRepository, IMapper mapper, IQuoteRepository quoteRepo)
    {
        _userManager = userManager;
        _reviewRepo = reviewRepository;
        _mapper = mapper;
        _quoteRepo = quoteRepo;
    }

    [HttpDelete("DeleteUser")]
    public async Task<IActionResult> DeleteUser(string Id)
    {
        var user = await _userManager.FindByIdAsync(Id);

        if (user != null)
        {
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok("User Deleted");
            }
        }

        return BadRequest();
    }

    /// <summary>
    /// Updates user information
    /// </summary>
    /// <param name="userDetails"></param>
    /// <returns>Returns the updated information</returns>

    [SwaggerOperation(Summary = "Updates user details")]
    [HttpPut("UpdateUserAccount")]
    [Authorize(Roles = "Lawyer", AuthenticationSchemes = "Bearer")]
    [SwaggerResponseExample(400, typeof(BadUserUpdateExampleDetailsForCustomer))]
    [SwaggerResponseExample(200, typeof(GoodUserUpdateExampleDetailsForCustomer))]
    public async Task<IActionResult> UpdateUser(CustomerAccountForCreationDto userDetails)
    {
        var user = await _userManager.FindByEmailAsync(userDetails.Email);
        if (user != null)
        {
            user.Email = userDetails.Email;
            user.UserName = userDetails.BusinessEntityName;
            user.PasswordHash = userDetails.Password;
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

    //[SwaggerOperation(Summary = "Create a Review with this endpoint")]
    //[HttpPost("create")]
    //[Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
    //public ActionResult CreateReview([FromBody] Review reviewForCreation)
    //{
    //    // use this to get user Id From request and
    //    var review = _reviewRepo.CreateReview(reviewForCreation);
    //    return Ok(review);
    //}

    [SwaggerOperation(Summary = "Get all reviews for Admin")]
    [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
    [HttpGet("reviews")]
    [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any)]
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
    public async Task<ActionResult> SuccessReview()
    {
        var reviews = await _reviewRepo.GetAllSuccessfulReviews();
        return Ok(reviews);
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
    [Route("reviews/{reviewId}")]
    public ActionResult UpdateReview([FromQuery] Guid reviewId, [FromBody] ReviewForUpdateDTO review)
    {
        var reviews = _reviewRepo.UpdateReview(review, reviewId);
        if (review == null)
        {
            return BadRequest();
        }
        return Ok("Review was successfully updated");
    }

    [SwaggerOperation(Summary = "delete a review by an Admin")]
    [HttpDelete]
    [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
    [Route("{reviewId}/delete")]
    public ActionResult DeleteReview(Guid reviewId)
    {
        _reviewRepo.DeleteReview(reviewId);
        _reviewRepo.Save();
        return Ok("Review is successfully deleted");
    }

    [SwaggerOperation(Summary = "Gets Quotes for the admin")]
    [HttpGet("quote/{quoteId}")]
    [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
    public ActionResult GetQuote(Guid quoteId)
    {
        Quote quote = _quoteRepo.GetQuoteById(quoteId);
        var quoteToDisplay = _mapper.Map<QuoteForCreationDto>(quote);
        return Ok(quoteToDisplay);
    }

    [HttpGet("quotes")]
    public ActionResult Getall()
    {
        return Ok(_quoteRepo.GetAll().ToList());
    }

    [SwaggerOperation(Summary = "Gets details of all the Lawyers in csv")]
    [HttpGet("get_contact_details_of_lawyer_as_csv")]
    public IActionResult ExportContactDetailsAsCSV()
    {
        var users = _userManager.GetUsersInRoleAsync("Lawyer").Result.ToList()
            .Select(user => new LawyersContactDetailDto()
            {
                FullName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber is null ? "null" : user.PhoneNumber,
            });
        var stream = new MemoryStream();
        using (var writeFile = new StreamWriter(stream, leaveOpen: true))
        {
            var csv = new CsvWriter(writeFile, CultureInfo.InvariantCulture);
            csv.WriteRecords(users);
        }
        stream.Position = 0; //reset stream
        return File(stream, "application/octet-stream", "Details.csv");
    }
}