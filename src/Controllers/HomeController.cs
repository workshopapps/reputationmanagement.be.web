using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using src.Entities;
using src.Models;
using src.Models.Dtos;
using src.Models.ExampleModels;
using src.Services;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
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

        public HomeController(IReviewRepository reviewRepo, 
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            _reviewRepo = reviewRepo;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
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
        /// Returns the reviews posted by the currently signed in user
        /// </summary>
        /// <returns>a list of reviews</returns>
        [SwaggerOperation(Summary = "Returns the reviews posted by the currently signed in user")]
        [HttpGet("postedreviews")]
        public ActionResult<IEnumerable<ReviewForDisplayDto>> PostedReviews()
        {
            //todo
            return Ok();
        }

     
      

        [HttpGet("/api/reviews/{reviewId}")]
        [Authorize(Roles = "Customer", AuthenticationSchemes ="Bearer")]
        public IActionResult GetSingleReview(Guid reviewId)
        {
            if (reviewId == Guid.Empty)
            {
                return BadRequest();
            } 
            Review singleReview = _reviewRepo.GetReviewById(reviewId);

            if (singleReview == null)
                return NotFound();

            return Ok(singleReview);
        }

        /// <summary>
        /// Create a Review with this endpoint
        /// </summary>
        /// <param name="CreateReview"></param>
        /// <returns></returns>
        [SwaggerOperation(Summary = "Create a Review with this endpoint")]
        [HttpPost("review")]
        [Authorize(Roles = "Customer", AuthenticationSchemes = "Bearer")]
        public ActionResult CreateReview([FromBody] ReviewForCreationDto reviewForCreationDto)
        {
            var review = _reviewRepo.CreateReviews(reviewForCreationDto);
            return Ok(review);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="review"></param>
        /// <returns>Review is successfully updated</returns>

        [SwaggerOperation(Summary = "Update a review by an User")]
        [HttpPut]
        [Authorize(Roles = "Customer", AuthenticationSchemes = "Bearer")]
        [Route("{reviewId}/edit")]
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
        [SwaggerOperation(Summary = "delete a review by a user")]
        [HttpDelete]
        [Authorize(Roles = "Customer", AuthenticationSchemes = "Bearer")]
        [Route("{reviewId}")]
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
        [SwaggerOperation(Summary = "delete multiple reviews by a User")]
        [HttpDelete]
        [Authorize(Roles = "Customer", AuthenticationSchemes = "Bearer")]
        [Route("{userId}")]
        public ActionResult DeleteReviews(Guid userId)
        {

          _reviewRepo.DeleteReviews(userId); 
            _reviewRepo.Save();
            return Ok("Reviews successfully deleted");
        }


        


        [HttpPost("postreview")]
        [Authorize(Roles = "Customer", AuthenticationSchemes = "Bearer")]
        public IActionResult Postreview(Review review)
        {
            if (review == null)
            {
                return BadRequest("Review object is not passed or added");
            }

            _reviewRepo.CreateSaveReview(review);

            return Ok("Review successfully added");

        }

        /// <summary>
        /// Deletes All Reviews Associated With this user
        /// </summary>
        /// <returns>Ok</returns>
        [SwaggerOperation(Summary = "Deletes All Reviews Associated With this user")]
        [HttpDelete("reviews/delete-all-reviews")]
        public IActionResult DeleteReviews()
        {
            //todo
            var userId = new Guid(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            _reviewRepo.DeleteReviews(userId);
            _reviewRepo.Save();

            return Ok();
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

        [HttpGet("GetUpdatedReviews")]
        [Authorize(Roles = "Customer", AuthenticationSchemes = "Bearer")]
        public IActionResult GetUpdatedReviews(Guid UserId)
        {
            var updatedReviews = _reviewRepo.GetUpdatedReviews(UserId);

            if (updatedReviews == null)
            {
                return NotFound();
            }

            return Ok(updatedReviews);

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
    }
}
