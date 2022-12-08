using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using src.Data;
using src.Entities;
using src.Models.Dtos;
using src.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace src.Controllers
{

    /// <summary>
    /// Provides functionality for anonymous users.
    /// </summary>
    [Route("api")]
    [ApiController]
    [SwaggerTag("For Regular(normal) site users")]
    public class AnonymousUserController : Controller
    {

        private readonly IBufferedFileUploadService _bufferedFileUploadService;
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        public AnonymousUserController(IBufferedFileUploadService bufferedFileUploadServices, ApplicationDbContext context, IEmailSender emailSender)
        {
            _bufferedFileUploadService = bufferedFileUploadServices;
            _context = context;
            _emailSender = emailSender; 
        }

        /// <summary>
        /// This endpoint allows an anonymous user, usually a lawyer to send an application to a user
        /// </summary>
        /// <param name="careerResponse">Lawyer registeration details sent as a request</param>
        /// <returns code="200">Response sent successful, await review</returns>
        /// <returns code="400"></returns>
        [SwaggerOperation(Summary = "Sends a response for a lawyer application from the career page")]
        [HttpPost]
        [Route("apply")]
        public async Task<IActionResult> CreateResponse([FromForm] CareerResponseDto careerResponse)
        {
            const string EMAIL_SUBJECT = "Career Application Acknowledgement";
            const string EMAIL_BODY = "Response recieved successfully, we'll get back to you shortly";
            string coverPath = await _bufferedFileUploadService.SaveFile(careerResponse.coverLetter, "CoverLetterUpload");
            string resumePath = await _bufferedFileUploadService.SaveFile(careerResponse.resume, "ResumeUpload");

            if (careerResponse != null)
            {
                var response = new CareerResponse()
                {
                    CoverLetterFileName = coverPath,
                    ResumeFileName = resumePath,
                    FirstName = careerResponse.firstName,
                    LastName = careerResponse.lastName,
                    Email = careerResponse.email,
                    PhoneNumber = careerResponse.phoneNumber,
                    Position= careerResponse.position,
                    Reason = careerResponse.reason,
                };
                _context.CareerResponses.Add(response);

                await _context.SaveChangesAsync();
                await _emailSender.SendEmailAsync(careerResponse.email, EMAIL_SUBJECT, EMAIL_BODY);
                return Ok("Response sent successful, await review");
            }
            return BadRequest();
        }
    }
}
