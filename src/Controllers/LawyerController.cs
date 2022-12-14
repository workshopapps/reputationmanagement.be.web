using AutoMapper;
using CsvHelper;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using src.Entities;
using src.Models.Dtos;
using src.Models.ExampleModels;
using src.Services;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Globalization;
using System.Security.Claims;

namespace src.Controllers
{
    /// <permission cref="lawyer">description</permission>
    [SwaggerTag("For Lawyer Authorization")]
    [ApiController]
    [Route("api/lawyer")]
    [Authorize(Roles = "Lawyer", AuthenticationSchemes = "Bearer")]
    public class LawyerController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LawyerController(IReviewRepository reviewRepo,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper, IEmailSender emailSender)
        {
            _reviewRepo = reviewRepo;
            _mapper = mapper;
            _emailSender = emailSender;
            _userManager = userManager;
        }

        /// <summary>
        /// Greets the lawyer
        /// </summary>
        /// <returns>Hello lawyer!</returns>
        [SwaggerOperation(Summary = "Greets the lawyer")]
        [HttpGet("greet")]
        [AllowAnonymous]
        public ActionResult greet()
        {
            string greetings = "Hello lawyer!";
            return Ok(greetings);
        }

        /// <summary>
        /// Provides all the reviews for the lawyer, with pagination to avoid query delay
        /// </summary>
        /// <param name="reviewForUpdate"></param>
        /// <returns>A review</returns>
        [SwaggerOperation(Summary = "Update a review by Lawyer")]
        [HttpPatch("review/{reviewId}")]
        [Authorize(Roles = "Lawyer", AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> UpdateReview(Guid reviewId, JsonPatchDocument<LawyerReviewForUpdateDTO> reviewForUpdatePatchDoc)
        {
            try
            {
                if (reviewForUpdatePatchDoc is not null)
                {
                    var newReviewForUpdate = new LawyerReviewForUpdateDTO();
                    reviewForUpdatePatchDoc.ApplyTo(newReviewForUpdate);

                    if (ModelState.IsValid is false)
                    {
                        return BadRequest(ModelState);
                    }
                    var updatedReview = _reviewRepo.UpdateReviewLawyer(newReviewForUpdate, reviewId);
                    var reviewForDisplay = _mapper.Map<ReviewForDisplayDto>(updatedReview);

                    string emailSubject = "Review status update";
                    var userToNotify = await _userManager.FindByIdAsync(updatedReview.UserId.ToString());
                    
                    await _emailSender.SendEmailAsync(userToNotify.Email, $"{emailSubject}",
                         $"The status of your review with id of \"{updatedReview.ReviewId}\" and review string of \"{updatedReview.ReviewString}\" from {updatedReview.Email} has been updated to " +
                         $"\"{updatedReview.Status}\"" + $" check https://repute.hng.tech/request?requestId={updatedReview.ReviewId} to view the review");
                    return new ObjectResult(reviewForDisplay);
                }
            }
            catch (SmtpCommandException ex)
            {
                return BadRequest(ex.Message);
            }

            return BadRequest(ModelState);

        }

        /// <summary>
        /// Returns all the successful reviews
        /// </summary>
        /// <returns>Returns all reviews with a statusType of pending</returns>
        [SwaggerOperation(Summary = "Get all successful reviews for Lawyer (statusType = successful)")]
        [HttpGet]
        [Authorize(Roles = "Lawyer", AuthenticationSchemes = "Bearer")]
        [Route("SuccessfulReview")]
        public async Task<ActionResult> SuccessReview()
        {
            var successfulReviews = await _reviewRepo.GetAllSuccessfulReviews() as List<ReviewForDisplayDto>;
            return Ok(successfulReviews);
        }

        /// <summary>
        /// Provides all the reviews for the lawyer, with pagination to avoid query delay
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>A list of all Reviews</returns>
        [SwaggerOperation(Summary = "Get all reviews for Lawyer")]
        [Authorize(Roles = "Lawyer", AuthenticationSchemes = "Bearer")]
        [HttpGet("reviews")]
        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any)]
        public ActionResult GetAllReviews([FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 10)
        {
            var reviews = _reviewRepo.GetReviews(pageNumber, pageSize).ToList();
            var reviewsToReturn = _mapper.Map<IEnumerable<ReviewForDisplayDto>>(reviews) as List<ReviewForDisplayDto>;
            return Ok(reviewsToReturn);
        }

        /// <summary>
        /// Gets a review by id
        /// </summary>
        /// <param name="reviewId"></param>
        /// <returns>A single review</returns>
        [SwaggerOperation(Summary = "Gets a single review by id")]
        [HttpGet("reviews/{reviewId}")]
        [Authorize(Roles = "Lawyer", AuthenticationSchemes = "Bearer")]
        [SwaggerResponseExample(200, typeof(GoodSingleReviewExample))]
        public ActionResult GetSingleReview(Guid reviewId)
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

        /// <summary>
        /// Returns all the inconclusive reviews
        /// </summary>
        /// <returns>All reviews with a statusType of inconclusive</returns>
        [SwaggerOperation(Summary = "Gets all inconclusive reviews for lawyer (statusType = inconclusive)")]
        [HttpGet("inconclusiveReviews")]
        [Authorize(Roles = "Lawyer", AuthenticationSchemes = "Bearer")]
        public IActionResult GetAllInconclusiveReviews()
        {
            var inconclusiveReviews = _reviewRepo.GetInconclusiveReviews();
            return Ok(inconclusiveReviews);
        }

        /// <summary>
        /// Returns all the pending reviews
        /// </summary>
        /// <returns>Pending reviews</returns>
        [SwaggerOperation(Summary = "Gets all Pending reviews for lawyer (statusType = Pending)")]
        [HttpGet("PendingReview")]
        [Authorize(Roles = "Lawyer", AuthenticationSchemes = "Bearer")]
        public IActionResult UnclaimedReviews()
        {
            var pendingReviews = _reviewRepo.GetPendingReview();
            var reviewsForDisplay = _mapper.Map<List<ReviewForDisplayDto>>(pendingReviews);
            if (pendingReviews == null)
            {
                return NotFound("No pending reviews");
            }
            return Ok(reviewsForDisplay);
        }

        /// <summary>
        /// Get Reviews by Status Type
        /// </summary>
        /// <param name="status"></param>
        /// <returns>Reviews of the same statusType provided</returns>
        [SwaggerOperation(Summary = "Reviews By StatusType, PendingReview = 0, Successful = 1, Inconclusive = 2, Failed = 3")]
        [HttpGet("GetReviewByStatus")]
        [Authorize(Roles = "Lawyer", AuthenticationSchemes = "Bearer")]
        [SwaggerResponseExample(200, typeof(GoodReviewByStatusForCustomer))]
        public IActionResult GetReviewByStatus([FromQuery] StatusType status)
        {
            var reviewByStatus = _reviewRepo.GetReviewByStatusType(status);

            if (reviewByStatus == null)
            {
                return NotFound("No reviews with this status");
            }

            return Ok(reviewByStatus);
        }

        /// <summary>
        /// Allows for sending of email to users of selected bad reviews
        /// </summary>
        /// <param name="emailData">destructured as {EmailId-> for user email, EmailBody -> for email content}</param>
        /// <returns>Reviews of the same statusType Provided</returns>
        [SwaggerOperation(Summary = "Sends email to the user from reviewer")]
        [HttpPost("email/create")]
        [Authorize(Roles = "Lawyer", AuthenticationSchemes = "Bearer")]
        [SwaggerResponseExample(400, typeof(BadSendingEmailTExample))]
        [SwaggerResponseExample(200, typeof(GoodSendingEmailTExample))]
        public ActionResult SendEmail([FromBody] EmailDataDto emailData)
        {
            const string EMAIL_SUBJECT = "Plea for removal of review";
            try
            {
                _emailSender.SendEmailAsync(emailData.EmailToId, EMAIL_SUBJECT, emailData.EmailBody);
                return Ok("Success");
            }
            catch (SmtpCommandException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [SwaggerOperation(Summary = "Lawyer claims a review.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("ClaimReview")]
        public ActionResult ClaimReview(Guid reviewId)
        {
            if (reviewId == null)
            {
                return BadRequest();
            }
            var LawyerEmail = this.User.FindFirstValue(ClaimTypes.Name);
            var result = _reviewRepo.ClaimReview(reviewId, LawyerEmail);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok();
        }

        [SwaggerOperation(Summary = "Getv all reviews claimed by lawyer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetClaimedReviews")]
        public ActionResult GetClaimedReviews()
        {
            var LawyerEmail = this.User.FindFirstValue(ClaimTypes.Name);
            var result = _reviewRepo.GetClaimedReviews(LawyerEmail);
            return Ok(result);
        }

        [SwaggerOperation(Summary = "Get all reviews by company name")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("reviews/search")]
        public ActionResult GetReviewByBusinessName([FromQuery] string businessEntityName)
        {
            var reviews = _reviewRepo.GetReviewsByBusinessName(businessEntityName).ToList();
            var reviewsToReturn = _mapper.Map<List<ReviewForDisplayDto>>(reviews);
            return Ok(reviewsToReturn);
        }
    }
}