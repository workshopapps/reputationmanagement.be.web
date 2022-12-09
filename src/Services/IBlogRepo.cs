using src.Entities;
using src.Models.Dtos;

namespace src.Services
{
    public interface IBlogRepo
    {
        BlogEntryForDisplayDto GetBlogEntryById(int id);
        IEnumerable<BlogEntryForDisplayDto> GetBlogEntries(int pageNumber=0, int pageSize=10, string? userGuid = null);
        public Task<BlogEntryForDisplayDto> AddBlogEntry(BlogEntryForCreationDto blogEntry);


    }
}
