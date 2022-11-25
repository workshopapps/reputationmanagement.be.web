using AutoMapper;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using src.Entities;
using src.Helpers;
using src.Models;
using src.Models.Dtos;
using src.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;

namespace src.Controllers
{
    [ApiController]
    [Route("api/lawyer")]
    [Authorize(Roles = "Lawyer", AuthenticationSchemes = "Bearer")]
    public class LawyerController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;

        public LawyerController(IReviewRepository reviewRepo,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper, IEmailSender emailSender)
        {
            _reviewRepo = reviewRepo;
            _mapper = mapper;
            _emailSender = emailSender;
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
           var reviews =_reviewRepo.UpdateReviewLawyer(review);
            if (reviews == null)
            {
                return NotFound();
            }
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
        public ActionResult<List<ReviewForDisplayDto>> GetAllReviews([FromQuery]int pageNumber = 0, [FromQuery]int pageSize = 10)
        {
            var reviews = _reviewRepo.GetReviews(pageNumber, pageSize).ToList();
            var reviewsToReturn = _mapper.Map<IEnumerable<ReviewForDisplayDto>>(reviews) as List<ReviewForDisplayDto>;
            var json = JsonSerializer.Serialize(reviewsToReturn);
            return Ok(json);

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
            var inconclusiveReviews = _reviewRepo.GetInconclusiveReviews();
            return Ok(inconclusiveReviews);
        }


        [SwaggerOperation(Summary = "Sends email to the user from reviewer")]
        [HttpPost("email/create")]
        [Authorize(Roles = "Lawyer", AuthenticationSchemes = "Bearer")]
        public ActionResult SendEmail(EmailDataDto emailData)
        {
            const string EMAIL_SUBJECT = "Plea for removal of review";

            try
            {
                _emailSender.SendEmailAsync(emailData.EmailToId, EMAIL_SUBJECT, emailData.EmailBody);
                return Ok();
            }
            catch(SmtpCommandException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

    }
}
