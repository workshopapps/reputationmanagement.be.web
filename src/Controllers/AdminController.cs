using AutoMapper;
using CsvHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
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
    private readonly IAdminRepository _adminRepository;

    public AdminController(UserManager<ApplicationUser> userManager, IReviewRepository reviewRepository, IMapper mapper, IQuoteRepository quoteRepo, IAdminRepository adminRepository)
    {
        _userManager = userManager;
        _reviewRepo = reviewRepository;
        _mapper = mapper;
        _quoteRepo = quoteRepo;
        _adminRepository = adminRepository;
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
    [HttpPatch("UpdateUserAccount/{userEmail}")]
    [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
    [SwaggerResponseExample(400, typeof(BadUserUpdateExampleDetailsForCustomer))]
    [SwaggerResponseExample(200, typeof(GoodUserUpdateExampleDetailsForCustomer))]
    public async Task<ActionResult> UpdateUser(string userEmail, [FromBody] JsonPatchDocument<CustomerUpdateDto> userDetails)
    {
        if (userDetails != null)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            var userToPatch = _mapper.Map<CustomerUpdateDto>(user);
            try
            {
                userDetails.ApplyTo(userToPatch);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);  
            }

            var _ = _mapper.Map(userToPatch, user);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updateUserCredentialsResult = await _userManager.UpdateAsync(user);
            if (updateUserCredentialsResult.Succeeded)
            {
                return Ok();
            }
        }

        return BadRequest("unsuccessful");
    }

    [SwaggerOperation(Summary = "Get all reviews for Admin")]
    [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
    [HttpGet("reviews")]
    public ActionResult GetAllReviews(int pageNumber = 0, int pageSize = 10)
    {
        var reviews = _reviewRepo.GetAllReviews(pageNumber, pageSize).ToList();
        //var reviewsToReturn = _mapper.Map<IEnumerable<ReviewForDisplayDto>>(reviews);
        return Ok(reviews);
    }

    [SwaggerOperation(Summary = "Get a particular review for Admin")]
    [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
    [HttpGet("review/{reviewId}")]
    public ActionResult<Review> GetSingleReview(Guid reviewId)
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

    [SwaggerOperation(Summary = "Update a review for an admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPatch("review/{reviewId}")]
    public ActionResult<Review> EditReview(Guid reviewId, [FromBody] JsonPatchDocument<ReviewForUpdateDTO> reviewPatchDoc)
    {
        if (reviewPatchDoc is not null)
        {
            var reviewToPatch = _reviewRepo.GetReviewById(reviewId);
            var reviewForUpdateToPatch = _mapper.Map<ReviewForUpdateDTO>(reviewToPatch);

            try
            {
                reviewPatchDoc.ApplyTo(reviewForUpdateToPatch);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
                    }
            if (ModelState.IsValid is false)
            {
                return BadRequest(ModelState);
            }
            var review = _reviewRepo.UpdateReview(reviewForUpdateToPatch, reviewId);
            var updatedReview = _reviewRepo.GetReviewById(reviewId);

            return Ok(updatedReview);
        }
        return BadRequest(ModelState);
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

    [SwaggerOperation(Summary = "Gets All lawyers by an Admin")]
    [HttpGet("users/lawyers")]
    [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
    public async Task<ActionResult<IEnumerable<UserForDisplayDto>>> GetAllLawyers()
    {
        var lawyers = _mapper.Map<IEnumerable<UserForDisplayDto>>(await _adminRepository.GetAllLawyers());
        return Ok(lawyers);
    }

    [SwaggerOperation(Summary = "Gets All customers by an Admin")]
    [HttpGet("users/customers")]
    [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
    public async Task<ActionResult<IEnumerable<UserForDisplayDto>>> GetAllCustomers()
    {
        var customers = _mapper.Map<IEnumerable<UserForDisplayDto>>(await _adminRepository.GetAllCustomers());
        return Ok(customers);
    }

    [SwaggerOperation(Summary = "Gets user by Id")]
    [HttpGet("user/{userId}")]
    [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
    public async Task<ActionResult<UserForDisplayDto>> GetCustomerById(Guid userId)
    {
        var customer = await _userManager.FindByIdAsync(userId.ToString());
        var customerToDisplay = _mapper.Map<UserForDisplayDto>(customer);
        var customerIsInRoleResult = await _userManager.IsInRoleAsync(customer, "Customer");
        if (customerToDisplay is not null && customerIsInRoleResult)
        { return Ok(customerToDisplay); }
        return BadRequest($"user with Id \"{userId}\" does not exist or is not a customer");
    }

    [SwaggerOperation(Summary = "Gets reviews by a user email")]
    [HttpGet("reviews/{userEmail}")]
    [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
    public async Task<ActionResult<IEnumerable<ReviewForDisplayDto>>> GetReviewsCustomerByEmail(string userEmail,
    int pageNumber = 0, int pageSize = 10)
    {
        var reviews = _reviewRepo.GetReviewsByCustomerEmail(userEmail, pageNumber, pageSize);

        if (reviews is not null)
        {
            //var reviewsToDisplayForAdmin = _mapper.Map<IEnumerable<ReviewForDisplayDto>>(reviews);
            return Ok(reviews);
        }
        return BadRequest("No review for this user");
    }

    [SwaggerOperation(Summary = "Count all review created by a customer, use this endpoint for pagination")]
    [HttpGet("reviews/{userEmail}/count")]
    [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
    public async Task<ActionResult<int>> CountReviewsCustomerByEmail(string userEmail)
    {
        var count = await _reviewRepo.CountCustomerReviews(userEmail);
        return count;
    }

    [SwaggerOperation(Summary = "Gets reviews claimed by a lawyer")]
    [HttpGet("claimed-reviews/{lawyerEmail}")]
    [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
    public async Task<ActionResult<IEnumerable<Review>>> GetReviewsLawyerByEmail(string lawyerEmail,
    int pageNumber = 0, int pageSize = 10)
    {
        var reviews = await _reviewRepo.GetReviewsByLawyerEmail(lawyerEmail, pageNumber, pageSize);
        return Ok(reviews.ToList());
    }

    [SwaggerOperation(Summary = "Count all reviews claimed by lawyer, user this for pagination")]
    [HttpGet("claimed-reviews/{lawyerEmail}/count")]
    [Authorize(Roles = "Administrator", AuthenticationSchemes = "Bearer")]
    public async Task<ActionResult<int>> CountReviewsLawyerByEmail(string lawyerEmail)
    {
        var count = await _reviewRepo.CountLawyerReviews(lawyerEmail);
        return count;
    }

    [SwaggerOperation(Summary = "Re-assign review to another lawyer")]
    [HttpPatch("ReassignReview")]
    public ActionResult ReassignRewview(Guid reviewId, string lawyerEmail)
    {
        var review = _reviewRepo.ReassignReview(reviewId, lawyerEmail);
        if (review == null)
        {
            return BadRequest();
        }
        return Ok(review);
    }

    [SwaggerOperation(Summary = "Gets full review")]
    [HttpGet("FullReview")]
    public ActionResult GetFullReview(Guid reviewId)
    {
        var review = _reviewRepo.GetFullReview(reviewId);
        if (review == null)
        {
            return BadRequest();
        }
        return Ok(review);
    }

    [SwaggerOperation(Summary = "Return the list of all Lawyer contacts in csv")]
    [HttpGet("GetAllLawyerDetails")]
    public ActionResult ExportAllLawyerContactAsCSV()
    {
        var users = _userManager.GetUsersInRoleAsync("Lawyer").Result
            .Select(res => new LawyersContactDetailDto()
            {
                UserName = res.UserName,
                Email = res.Email,
                PhoneNumber = res.PhoneNumber is null ? "null" : res.PhoneNumber
            }).ToList();
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