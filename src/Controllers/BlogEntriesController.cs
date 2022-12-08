using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Entities;
using src.Models.Dtos;
using src.Services;

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

        public BlogEntriesController(IBufferedFileUploadService bufferedFileUploadServices, ApplicationDbContext context, IMapper mapper, IBlogRepo blogRepo)
        {
            _bufferedFileUploadService = bufferedFileUploadServices;
            _context = context;
            _mapper = mapper;
            _blogRepo = blogRepo;
        }
        // GET: api/BlogEntries/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetBlogEntry(Guid id)
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
    }
}
