using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Entities;

namespace src.Controllers
{
    [Route("api/blogging")]
    [ApiController]
    public class BlogEntriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BlogEntriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BlogEntries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogEntry>>> GetBlogEntries()
        {
            return await _context.BlogEntries.ToListAsync();
        }

        // GET: api/BlogEntries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogEntry>> GetBlogEntry(string id)
        {
            var blogEntry = await _context.BlogEntries.FindAsync(id);

            if (blogEntry == null)
            {
                return NotFound();
            }

            return blogEntry;
        }

        // PUT: api/BlogEntries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogEntry(string id, BlogEntry blogEntry)
        {
            if (id != blogEntry.BlogEntryId)
            {
                return BadRequest();
            }

            _context.Entry(blogEntry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogEntryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BlogEntries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BlogEntry>> PostBlogEntry(BlogEntry blogEntry)
        {
            _context.BlogEntries.Add(blogEntry);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BlogEntryExists(blogEntry.BlogEntryId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBlogEntry", new { id = blogEntry.BlogEntryId }, blogEntry);
        }

        // DELETE: api/BlogEntries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogEntry(string id)
        {
            var blogEntry = await _context.BlogEntries.FindAsync(id);
            if (blogEntry == null)
            {
                return NotFound();
            }

            _context.BlogEntries.Remove(blogEntry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlogEntryExists(string id)
        {
            return _context.BlogEntries.Any(e => e.BlogEntryId == id);
        }
    }
}
