using AutoMapper;
using Microsoft.AspNetCore.Identity;
using src.Data;
using src.Entities;
using src.Models.Dtos;
using System.IO;

namespace src.Services
{
    public class BlogEntryRepo : IBlogRepo
    {
        public readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public readonly IBufferedFileUploadService _bufferedFileUploadService;
        public BlogEntryRepo(ApplicationDbContext context, IMapper mapper, IBufferedFileUploadService bufferedFileUploadService)
        {
            _context = context;
            _mapper = mapper;
            _bufferedFileUploadService = bufferedFileUploadService;
        }
        public async Task<BlogEntryForDisplayDto> AddBlogEntry(BlogEntryForCreationDto blogEntry)
        {
            var blogEntryToAdd = _mapper.Map<BlogEntry>(blogEntry);
            blogEntryToAdd.PathToImage =Path.GetFullPath("../", Path.Combine(Environment.CurrentDirectory, "BlogPostImages"));
            
            var blogEntryToDisplay = _mapper.Map<BlogEntryForDisplayDto>(blogEntryToAdd);
            string coverPath = await _bufferedFileUploadService.SaveFile(blogEntry.Image, "Image", blogEntryToAdd.PathToImage);
            return blogEntryToDisplay;
        }

        public IEnumerable<BlogEntryForDisplayDto> GetBlogEntries(int pageNumber, int pageSize, string? userGuid = null)
        {
            int defaultPageSize = 10;
            int defaultPageNumber = 0;
            int maxPageSize = 100;
            if (pageSize > maxPageSize || pageSize < 0)
            {
                pageSize = defaultPageSize;
            }
            int availableNumberOfPages = _context.BlogEntries.Count() / pageSize;
            if (pageNumber > availableNumberOfPages)
            {
                pageNumber = defaultPageNumber;
            }
            var blogEntriesToDisplay = _mapper.Map<IEnumerable<BlogEntryForDisplayDto>>(_context.BlogEntries.Skip(pageSize * pageNumber).Take(pageSize).ToList());

            return blogEntriesToDisplay;
        }


        public BlogEntryForDisplayDto GetBlogEntryById(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var blogEntry = _context.BlogEntries.Where(c => c.Id == id).SingleOrDefault();
            var blogEntryToReturn = _mapper.Map<BlogEntryForDisplayDto>(blogEntry);
            return blogEntryToReturn;
        }
    }
}
