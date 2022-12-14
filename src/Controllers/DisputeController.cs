using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using src.Models.Dtos;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using AutoMapper;
using src.Services;
using src.Entities;
using src.Data;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace src.Controllers
{
    [Route("api/disputes")]
    [ApiController]
    public class DisputeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDisputeRepo _disputeRepo;
        private readonly IEmailSender _emailSender;
        private readonly IReviewRepository _reviewRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public DisputeController(ApplicationDbContext context,
            IMapper mapper,IDisputeRepo disputeRepo, IEmailSender emailSender, UserManager<ApplicationUser> userManager, IReviewRepository reviewRepo)
        {
            _context = context;
            _mapper = mapper;
            _disputeRepo = disputeRepo;
            _reviewRepo = reviewRepo;
            _emailSender = emailSender;
            _userManager = userManager;
        }


        [SwaggerOperation(Summary = "Create dispute for Customer.")]
        [Authorize(Roles = "Customer", AuthenticationSchemes = "Bearer")]
        [HttpPost("/dispute")]
        public async Task<IActionResult> CreateDispute(DisputeForCreationDto dispute)
        {
            var userMail = User.FindFirstValue(ClaimTypes.Name);
            var user = await _userManager.FindByEmailAsync(userMail);
            if (dispute == null)
                return BadRequest();

            var createdComplaint = await _disputeRepo.CreateDispute(dispute, user.Id);
            
            if (createdComplaint == null)
                return NoContent();

            var complaintToDisplay = _mapper.Map<DisputeForDisplayForCustomerDto>(createdComplaint);
            
            if (user.ComplaintStatus == true)
            {
                string emailSubject = "Repute - Complaint Mail";
                string emailBody = $"<p>Your Complaint</p><p><em>\"{createdComplaint.ComplaintMessage}\"</em> has been recorded</p>";
                await _emailSender.SendEmailAsync(userMail, $"{emailSubject}", emailBody);
            }
            return Ok(complaintToDisplay);
        }
        [SwaggerOperation(Summary = "Get dispute by Id.")]
        [Authorize(Roles = "Lawyer,Administrator", AuthenticationSchemes = "Bearer")]
        [HttpGet("{disputeId}")]
        public async Task<IActionResult> GetDisputeById(string disputeId)
        {
            return Ok(_disputeRepo.GetDisputeById(disputeId));
        }
        [SwaggerOperation(Summary = "Get dispute by Id for a customer.")]
        [Authorize(Roles = "Customer", AuthenticationSchemes = "Bearer")]
        [HttpGet("customer/{disputeId}")]
        public async Task<ActionResult> GetDisputeByIdForCustomer(string disputeId)
        {
            return Ok(_disputeRepo.GetDisputeByIdForCustomer(disputeId));
        }


        [SwaggerOperation(Summary = "Get disputes for Lawyer user.")]
        [Authorize(Roles = "Lawyer", AuthenticationSchemes = "Bearer")]
        [HttpGet("lawyer")]
        public async Task<ActionResult<IEnumerable<DisputeForDisplayForLawyerDto>>> GetAllDisputesForLawyer([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 0)
        {
            var disputeForLawyer = _disputeRepo.GetAllDisputesForALawyer(this.User.FindFirstValue(ClaimTypes.Name), pageSize, pageNumber);
            return new ObjectResult(disputeForLawyer);
        }

        [SwaggerOperation(Summary = "Get dispute for customer user.")]
        [Authorize(Roles = "Customer", AuthenticationSchemes = "Bearer")]
        [HttpGet("Customer")]
        public async Task<ActionResult<IEnumerable<DisputeForDisplayForCustomerDto>>> GetAllDisputesForCustomer([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 0)
        {
            var disputeForCustomer = _disputeRepo.GetAllDisputesForCustomer(_userManager.FindByEmailAsync(this.User.FindFirstValue(ClaimTypes.Name)).Result.Id.ToString(),
                pageSize, pageNumber);
            return Ok(disputeForCustomer);
        }

        [SwaggerOperation(Summary = "update dispute status for lawyer")]
        [Authorize(Roles = "Lawyer", AuthenticationSchemes = "Bearer")]
        [HttpPut("lawyer/{disputeId}")]
        public async Task<ActionResult> GetAllDisputesForLawyer(string disputeId)
        {
            _disputeRepo.UpdateDisputeStatus(disputeId);
            var dispute = _context.Disputes.Find(disputeId);
            var customer = await _userManager.FindByIdAsync(dispute.UserId);
            if (customer.ComplaintStatus == true)
            {
                string userMail = customer.Email;
                string emailSubject = "Repute - Dispute Mail";
                string emailBody = $"<p>Your Dispute</p><p><em>\"{dispute.Complaint}\"</em> has been resolved</p>";
                await _emailSender.SendEmailAsync(userMail, $"{emailSubject}", emailBody);
            }
            return Ok();
        }
    }
}
