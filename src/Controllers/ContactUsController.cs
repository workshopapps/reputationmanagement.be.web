using Microsoft.AspNetCore.Mvc;
using src.Entities;
using src.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace src.Controllers;

[SwaggerTag("For Anonymous user messages")]
[ApiController]
[Route("api/contactUs")]
public class ContactUsController : ControllerBase
{
    private readonly IAnonContactRepository _contactRepo;

    public ContactUsController(IAnonContactRepository contactRepo)
    {
        _contactRepo = contactRepo;
    }

        /// <summary>
        /// Recieves message from user
        /// </summary>
        [SwaggerOperation(Summary = "customer send message")]
        [HttpPost("send")]
        public IActionResult SendMessage(ContactUs contactUs)
        {
            if(contactUs == null)
            {
                return BadRequest("No message");
            }

            _contactRepo.SendMessage(contactUs);
            return Ok("Message recieved");

        }
}