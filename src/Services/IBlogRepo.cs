using src.Entities;
using src.Models.Dtos;

namespace src.Services
{
    public interface IBlogRepo
    {
        Review GetBlogEntryById(Guid id);
        IEnumerable<BlogEntryForDisplayDto> GetBlogEntries(int pageNumber, int pageSize, string? userGuid = null);
        public Task<BlogEntryForDisplayDto> AddBlogEntry(BlogEntryForCreationDto blogEntry);


    }
}
