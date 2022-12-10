using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Entities;
using src.Models.Dtos;
using src.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace src.Controllers
{
    [Route("api/blogging")]
    [ApiController]
    public class BlogEntriesController : ControllerBase
    {
        private readonly IBufferedFileUploadService _bufferedFileUploadService;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IBlogRepo _blogRepo;
        private readonly IQuoteRepository _quoteRepo;
        private readonly IEmailSender _emailSender;

        public BlogEntriesController(IBufferedFileUploadService bufferedFileUploadServices, ApplicationDbContext context,
            IMapper mapper, IBlogRepo blogRepo, IQuoteRepository quoteRepo, IEmailSender emailSender)
        {
            _bufferedFileUploadService = bufferedFileUploadServices;
            _context = context;
            _mapper = mapper;
            _blogRepo = blogRepo;
            _quoteRepo = quoteRepo;
            _emailSender= emailSender;
        }
        // GET: api/BlogEntries/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetBlogEntry(int id)
        {
            var blogEntry = _blogRepo.GetBlogEntryById(id);

            if (blogEntry == null)
            {
                return NotFound();
            }

            return Ok(blogEntry);
        }
     
        [HttpGet]
        public async Task<ActionResult> GetBlogEntries([FromQuery] int pageNumber=0, [FromQuery] int pageSize=10)
        {
            var blogEntries = _blogRepo.GetBlogEntries(pageNumber, pageSize).ToList();
            return Ok(blogEntries);
        }
        // POST: api/BlogEntries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostBlogEntry([FromForm] BlogEntryForCreationDto blogEntry)
        {
            string imagePath = await _bufferedFileUploadService.SaveFile(blogEntry.Image, "BlogImageUpload");
            if (blogEntry != null)
            {
                var blogEntryForStorage = _mapper.Map<BlogEntry>(blogEntry);
                blogEntryForStorage.PathToImage = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, $"Uploads/BlogImageUpload/{blogEntry.Image.FileName}"));
                _context.BlogEntries.Add(blogEntryForStorage);
                await _context.SaveChangesAsync();
                return Ok("BlogEntryStored");
            }
            return BadRequest();
        }

        // POST: api/blogging/quote

        /// <summary>
        /// Create a Review with this endpoint
        /// </summary>
        /// <param name="CreateReview"></param>
        /// <returns></returns>
        [SwaggerOperation(Summary = "Create a Quote with this endpoint")]
        [HttpPost("quote")]
        [AllowAnonymous]
        public async Task<ActionResult> CreateQuote([FromBody] QuoteForCreationFromBlogDto quoteForCreationDto)
        {
            var quoteForDisplay = _quoteRepo.CreateQuoteFromBlog(quoteForCreationDto);

            var emailData = new EmailDataDto()
            {
                EmailToId = quoteForCreationDto.Email,
                EmailBody = StringTemplates.QuoteTemplate
            };
            string EMAIL_SUBJECT = "Follow up email from the form you filled at https://repute.hng.tech";
            try
            {
               await _emailSender.SendEmailAsync(emailData.EmailToId, EMAIL_SUBJECT, emailData.EmailBody);
               return CreatedAtAction("GetQuote", "Quote", new { quoteId = new Guid(quoteForDisplay.Id) }, quoteForDisplay);                 
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

    }
}
