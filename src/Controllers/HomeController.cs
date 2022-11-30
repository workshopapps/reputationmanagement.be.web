using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using src.Entities;
using src.Models;
using src.Models.Dtos;
using src.Services;
using Swashbuckle.AspNetCore.Annotations;
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

        public HomeController(IReviewRepository reviewRepo, 
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            _reviewRepo = reviewRepo;
            _mapper = mapper;
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

        /// <summary>
        /// Create a Review with this endpoint
        /// </summary>
        /// <param name="CreateReview"></param>
        /// <returns></returns>
        [SwaggerOperation(Summary = "Create a Review with this endpoint")]
        [HttpPost("create")]
        [Authorize(Roles = "Customer", AuthenticationSchemes = "Bearer")]
        public IActionResult CreateReview([FromBody] ReviewForCreationDto reviewForCreationDto)
        {
            var review = _reviewRepo.CreateReviews(reviewForCreationDto);
            return Ok(review);
           
        }

        [SwaggerOperation(Summary = "Update a review by an Admin")]
        [HttpPut]
        [Authorize(Roles = "Customer", AuthenticationSchemes = "Bearer")]
        [Route("{reviewId}/update")]
        public ActionResult EditReview([FromBody] ReviewForUpdateDTO review)
        {

            var reviews = _reviewRepo.UpdateReviewLawyer(review);
            if (review == null)
            {
                return NotFound();
            }
            return Ok("Review is successfully updated");

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
        /// <param name="reviewId"></param>
        /// <returns>Review is successfully deleted</returns>
        [SwaggerOperation(Summary = "delete a review by an User")]
        [HttpDelete]
        [Authorize(Roles = "Customer", AuthenticationSchemes = "Bearer")]
        [Route("review/{reviewId}/delete")]
        public IActionResult DeleteReviews(Guid reviewId)
        {
            _reviewRepo.DeleteReview(reviewId);
           var result = _reviewRepo.Save();
            if (!result)
            {
                return NotFound("Data not found");
            }
            return Ok("Review is successfully deleted");
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

      

    }
}
