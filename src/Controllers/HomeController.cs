using AutoMapper;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using src.Entities;
using src.Models;
using src.Models.Dtos;
using src.Models.ExampleModels;
using src.Services;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace src.Controllers
{
    
    [ApiController]
    [Authorize(Roles = "Customer", AuthenticationSchemes = "Bearer")]
    [Route("api")]
    public class HomeController:ControllerBase
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IContactUsMail _contactUsMail;
        private readonly IQuoteRepository _quoteRepo;

        public HomeController(IReviewRepository reviewRepo, 
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper, IQuoteRepository quoteRepo, IContactUsMail contactUsMail)
        {
            _reviewRepo = reviewRepo;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _quoteRepo = quoteRepo;
            _contactUsMail = contactUsMail;
        }

        /// <summary>
        /// Greets the user
        /// </summary>
        /// <returns>Hello customer!</returns>
        [SwaggerOperation(Summary = "Greets the customer")]
        [HttpGet("greet")]
        [AllowAnonymous]
        public IActionResult greet()
        {
           
            string greetings = "Hello customer!";
            return Ok(greetings);
        }

        /// <summary>
        /// Provides all the reviews for the lawyer, with pagination to avoid query delay
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>A list of all Reviews</returns>
        [SwaggerOperation(Summary = "Get all reviews for user")]
        [Authorize(Roles = "Customer", AuthenticationSchemes = "Bearer")]
        [HttpGet("reviews")]
        //[ResponseCache(NoStore = true, Duration = 0)]
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Client)]
        public ActionResult GetAllReviews([FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 10)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            var appUser = _userManager.FindByEmailAsync(userEmail).Result;
            var reviews = _reviewRepo.GetReviews(pageNumber, pageSize,appUser.Id).ToList();
            var reviewsToReturn = _mapper.Map<IEnumerable<ReviewForDisplayDto>>(reviews) as List<ReviewForDisplayDto>;
            return Ok(reviewsToReturn);
        }

        [HttpGet("review/{reviewId}")]
        [Authorize(Roles = "Customer", AuthenticationSchemes ="Bearer")]
        public ActionResult GetSingleReview(Guid reviewId)
        {
            if (reviewId == Guid.Empty)
            {
                return BadRequest();
            } 
            Review singleReview = _reviewRepo.GetReviewById(reviewId);

            if (singleReview == null)
                return NotFound();
            var reviewForDisplay = _mapper.Map<ReviewForDisplayDto>(singleReview);
            return Ok(reviewForDisplay);
        }

        /// <summary>
        /// Create a Review with this endpoint
        /// </summary>
        /// <param name="CreateReview"></param>
        /// <returns></returns>
        [SwaggerOperation(Summary = "Create a Review with this endpoint")]
        [Authorize(Roles = "Customer", AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("review")]
        public async Task<ActionResult> CreateReview([FromBody] ReviewForCreationDto reviewForCreationDto)
        {
            
            var userMail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            var user = await _userManager.FindByEmailAsync(userMail);
            var userId = new Guid(user.Id);
            
            var reviewForCreation = _mapper.Map<Review>(reviewForCreationDto);
            reviewForCreation.UserId = userId; 
            var reviewForDisplay = _reviewRepo.CreateReview(reviewForCreation);
            return CreatedAtAction(nameof(GetSingleReview), new {reviewId = reviewForDisplay.ReviewId}, reviewForDisplay);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="review"></param>
        /// <returns>Review is successfully updated</returns>

        [SwaggerOperation(Summary = "Update a review by an User")]
        [Authorize(Roles = "Customer", AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("review/{reviewId}")]
        public ActionResult EditReview([FromBody] ReviewForUpdateDTO review)
        {
            var reviews = _reviewRepo.UpdateReviewLawyer(review);
            if (review == null)
            {
                return NotFound();
            }
            return Ok("Review is successfully updated");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reviewId"></param>
        /// <returns>Review is successfully deleted</returns>
        [SwaggerOperation(Summary = "delete a review by a customer")]
        [Authorize(Roles = "Customer", AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("review/{reviewId}")]
        public ActionResult DeleteReview(Guid reviewId)
        {

            var result = _reviewRepo.DeleteReview(reviewId);
            if (!result)
            {
                return NotFound();
            }
            _reviewRepo.Save();
            return Ok("Review is successfully deleted");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Reviews successfully deleted</returns>     
        [SwaggerOperation(Summary = "delete multiple reviews by a Customer")]
        [Authorize(Roles = "Customer", AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("reviews")]
        public async Task<ActionResult> DeleteReviews()
        {
            var userMail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            var user = await _userManager.FindByEmailAsync(userMail);
            var userId = new Guid(user.Id);
            _reviewRepo.DeleteReviews(userId); 
            _reviewRepo.Save();
            return Ok("Reviews successfully deleted");
        }

        [SwaggerOperation(Summary = "Create Complaint for each User.")]
        [HttpPost]
        [Route("CreateComplaint")]
        public IActionResult CreateComplaint(CreateUserComplainsDto complains)
        {
            if (complains == null)
                return NoContent();

            var query = _reviewRepo.PostUserComplains(complains);
            if (query == null)
                return NoContent();

            return Ok(query);
        }
        [SwaggerOperation(Summary = "Notify user when a review's status changes ")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("reviews/updates")]
        [Authorize(Roles = "Customer", AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> GetUpdatedReviews()
        {
            var userMail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            var user = await _userManager.FindByEmailAsync(userMail);
            var userId = new Guid(user.Id);
           
            var updatedReviews = _reviewRepo.GetUpdatedReviews(userId).Result;

            if (updatedReviews == null)
            {
                return NotFound();
            }

            return Ok(updatedReviews);

        }


        [HttpPost]
        [Route("PostChallengeReviews")]
        public IActionResult CreateChallengeReviews(ChallengeUserReviewDto challenge)
        {
            if (challenge == null)
                return NoContent();

            var query = _reviewRepo.PostChallengeReview(challenge);
            if (query == null)
                return NoContent();

            return Ok(query);
        }


        /// <summary>
        /// Gets the language preference of the currently signed in user
        /// </summary>
        /// <returns>The Language preference of the signed in user</returns>
        [HttpGet("customer/language")]
        [SwaggerOperation(Summary = "Gets the language preference of the currently signed in user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetLanguage()
        {
            var userEmail = HttpContext.User.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value; 
            var user = await _userManager.FindByEmailAsync(userEmail);
            return Ok(user.Language);
        }

        [HttpPost("customer/language")]
        [SwaggerOperation(Summary = "Sets the language preference of the currently signed in user, select one of {\"english\", \"german\", \"russian\", \"chinese\"}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> SetLanguage([FromQuery] string language)
        {
            List<string> listOfSupportedLanguages = new() { "english", "german", "russian", "chinese" };
            
            if (listOfSupportedLanguages.Contains(language.ToLowerInvariant()))
            {
                var userEmail = HttpContext.User.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                var user = await _userManager.FindByEmailAsync(userEmail);
                user.Language = language.ToLowerInvariant();
                var updateResult = await _userManager.UpdateAsync(user);
                if (updateResult.Succeeded)
                {
                    return CreatedAtAction("GetLanguage", language);
                }
            }
            return BadRequest("Invalid language string, please select one of {\"english\", \"german\", \"russian\", \"chinese\"}");
        }

        [SwaggerOperation(Summary = "Create multiple reviews")]
        [HttpPost]
        [Route("UploadReviewsCSV")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UploadReviewsCSV(IFormFile file)
        {
            var data = _reviewRepo.ReviewsBulkUpload(file);

            return Ok("Reviews bulk upload added successfully");
        }

        [HttpPost("quote")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Create a quote for an unauthorised user")]

        public IActionResult CreateAQuote(QuoteForCreationDto quoteDto)
        {
            var quoteForCreation = _mapper.Map<Quote>(quoteDto);
            _quoteRepo.CreateQuote(quoteDto);       
            return Created($"api/Admin/quote/{quoteForCreation.Id}",quoteForCreation);
        }


        [HttpPost("ContactUs")]
        [SwaggerOperation(Summary = "Allow user to mail the admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> ContactUs(ContactUsEmailDto contactMsg)
        {
            try
            {
                _contactUsMail.SendEmailAsync(contactMsg.FromEmail, contactMsg.EmailSubject, contactMsg.EmailBody);
                return Ok("Success, message sent");
            }
            catch (SmtpCommandException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
