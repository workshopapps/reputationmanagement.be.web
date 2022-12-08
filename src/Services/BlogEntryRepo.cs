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
            throw new NotImplementedException();
        }

        public Review GetBlogEntryById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
