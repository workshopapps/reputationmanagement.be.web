using AutoMapper;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using src.Models.Dtos;
using src.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace src.Controllers
{
    [SwaggerTag("For Working With Quotes")]
    [ApiController]
    [Route("api/quote")]
    [Authorize(Roles = "Lawyer", AuthenticationSchemes = "Bearer")]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteRepository _quoteRepo;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;

        public QuoteController(IQuoteRepository quoteRepo,
            IMapper mapper, IEmailSender emailSender)
        {
            _quoteRepo = quoteRepo;
            _mapper = mapper;
            _emailSender = emailSender;
        }

        /// <summary>
        /// Create a Review with this endpoint
        /// </summary>
        /// <param name="CreateReview"></param>
        /// <returns></returns>
        [SwaggerOperation(Summary = "Create a Quote with this endpoint")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> CreateQuote([FromBody] QuoteForCreationDto quoteForCreationDto)
        {
            var quoteForDisplay = _quoteRepo.CreateQuote(quoteForCreationDto);

            var emailData = new EmailDataDto()
            {
                EmailToId = quoteForCreationDto.Email,
                EmailBody = "<p><h4>Hi, this is Leo with Repute</h4>\r\n\r\n " +
                "I am the Head of Sales and I have been assigned to your case.\r\n\r\n " +
                "I know you may have filled out an onboarding form, one of our agents will " +
                "reach out to you via a phone call to find out the best way we can help you. \r\n\r\nMany thanks,\r\n\r\n       " +
                "Leo Hyperion\r\n HEAD OF PARTNERSHIPS</p>\r\n"
            };
            string EMAIL_SUBJECT = "Follow up email from the form you filled at https://repute.hng.tech";
            try
            {
                _emailSender.SendEmailAsync(emailData.EmailToId, EMAIL_SUBJECT, emailData.EmailBody);
                return CreatedAtAction(nameof(GetQuote), new { quoteId = new Guid(quoteForDisplay.Id) }, quoteForDisplay);
            }
            catch (SmtpCommandException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                throw;
            }
        }

        [SwaggerOperation(Summary = "Get a Quote with this endpoint")]
        [HttpGet("{quoteId}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetQuote(Guid quoteId)
        {
            var quoteForDisplay = _quoteRepo.GetQuoteById(quoteId);
            if (quoteForDisplay == null) { return NotFound(); }
            return Ok(quoteForDisplay);
        }

        [SwaggerOperation(Summary = "Get all Quotes with this endpoint")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetQuotes()
        {
            var quotes = _quoteRepo.Quotes.Select(x => x).ToList();
            return Ok(quotes);
        }
    }
}