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

        public readonly IBufferedFileUploadService _bufferedFileUploadService;
        public readonly ApplicationDbContext _context;
        public AnonymousUserController(IBufferedFileUploadService bufferedFileUploadServices, ApplicationDbContext context)
        {
            _bufferedFileUploadService = bufferedFileUploadServices;
            _context = context;
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
            string coverPath = await _bufferedFileUploadService.SaveFile(careerResponse.CoverLetter, "CoverLetterUpload");
            string resumePath = await _bufferedFileUploadService.SaveFile(careerResponse.Resume, "ResumeUpload");

            if (careerResponse != null)
            {
                var response = new CareerResponse()
                {
                    CoverLetterFileName = coverPath,
                    ResumeFileName = resumePath,
                    FirstName = careerResponse.FirstName,
                    LastName = careerResponse.LastName,
                    Email = careerResponse.Email,
                    PhoneNumber = careerResponse.PhoneNumber,
                    Position= careerResponse.Position,
                    Reason = careerResponse.Reason,
                };
                _context.CareerResponses.Add(response);

                await _context.SaveChangesAsync();
                return Ok("Response sent successful, await review");
            }
            return BadRequest();
        }
    }
}
