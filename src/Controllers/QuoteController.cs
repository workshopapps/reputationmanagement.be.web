using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using src.Entities;
using src.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;
using src.Models.Dtos;
using System.Security.Claims;
using MailKit.Net.Smtp;
using src.Models;

namespace src.Controllers
{
    [SwaggerTag("For Lawyer Authorization")]
    [ApiController]
    [Route("api/quote")]
    [Authorize(Roles = "Lawyer", AuthenticationSchemes = "Bearer")]
    public class QuoteController : ControllerBase {

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
        [SwaggerOperation(Summary = "Create a Review with this endpoint")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> CreateQuote([FromBody] QuoteForCreationDto quoteForCreationDto)
        {

            var quoteForDisplay = _quoteRepo.CreateQuote(quoteForCreationDto);

            var emailData = new EmailDataDto()
            {
                EmailToId = quoteForCreationDto.Email,
                EmailBody = "We'll get back to you, your quote has been" +
                "recorded",
                
            };
            const string EMAIL_SUBJECT = "Repute - QuoteMail";
            try
            {
                _emailSender.SendEmailAsync(emailData.EmailToId, EMAIL_SUBJECT, emailData.EmailBody);
                return Ok("Success");
            }
            catch (SmtpCommandException ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction("/api/quote/{quoteId}", new { quoteId = quoteForDisplay.Id }, quoteForDisplay);
        }

        [HttpGet("{quoteId}")]
        [Authorize(Roles = "Lawyer", AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> CreateQuote(Guid quoteId)
        {

            return Ok();
        }



    }

}
