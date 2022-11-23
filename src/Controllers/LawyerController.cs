using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using src.Entities;
using src.Models.Dtos;
using src.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace src.Controllers
{
    [ApiController]
    [Route("api/lawyer")]
    [Authorize(Roles = "Lawyer", AuthenticationSchemes = "Bearer")]
    public class LawyerController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IMapper _mapper;

        public LawyerController(IReviewRepository reviewRepo,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            _reviewRepo = reviewRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Greets the lawyer
        /// </summary>
        /// <returns>Hello lawyer!</returns>
        [SwaggerOperation(Summary = "Greets the lawyer")]
        [HttpGet("greet")]
        [AllowAnonymous]
        public ActionResult updaterequest()
        {
            string greetings = "Hello lawyer!";
            return Ok(greetings);
        }

        [SwaggerOperation(Summary = "Update a review by Lawyer")]
        [HttpPatch]
        [Authorize(Roles = "Lawyer", AuthenticationSchemes = "Bearer")]
        public ActionResult UpdateReview(ReviewForUpdateDTO review)
        {
           _reviewRepo.UpdateReviewLawyer(review);
            return Ok("Review is successfully updated");
        }
        
        [HttpGet]
        [Authorize(Roles = "Lawyer", AuthenticationSchemes = "Bearer")]
        [Route("SuccessfulReview")]
        public async Task<ActionResult> SuccessReview()
        {
           var resultModel = new List<SuccessfulReviewsDto>();
           var query = await _reviewRepo.GetAllSuccessfulReview();
           return Ok(query);
        }


        [SwaggerOperation(Summary = "Get all reviews for Lawyer")]
        [Authorize(Roles = "Lawyer", AuthenticationSchemes = "Bearer")]
        [HttpGet("reviews")]
        public ActionResult<IEnumerable<ReviewForDisplayDto>> GetAllReviews(int pageNumber = 0, int pageSize = 10)
        {
            var reviews = _reviewRepo.GetReviews(pageNumber, pageSize).ToList();
            var reviewsToReturn = _mapper.Map<IEnumerable<ReviewForDisplayDto>>(reviews);
            return Ok(reviewsToReturn);

        }


        [HttpGet("reviews/{reviewId}")]
        [Authorize(Roles = "Lawyer", AuthenticationSchemes = "Bearer")]
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
        [Authorize(Roles = "Lawyer", AuthenticationSchemes = "Bearer")]
        public IActionResult GetAllInconclusiveReviews()
        {
            IEnumerable<Review>[] inconclusiveIsNull = new IEnumerable<Review>[] {};
            IEnumerable<Review> inconclusiveReviews = _reviewRepo.GetInconclusiveReviews();
            if (inconclusiveReviews == null || inconclusiveReviews.Count() < 1)
            {
                return Ok(inconclusiveIsNull);
            }
            return Ok(inconclusiveReviews);
        }

    }
}
