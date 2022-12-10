using src.Entities;
using src.Models.Dtos;

namespace src.Services
{
    public interface IQuoteRepository
    {
        public IQueryable<Quote> Quotes{ get; }
        public Quote CreateQuote(QuoteForCreationDto quoteForCreation);
        public Quote CreateQuoteFromBlog(QuoteForCreationFromBlogDto quoteForCreation);
        public Quote GetQuoteById(Guid id);
        public void Save();
        public IEnumerable<Quote> GetAll();

    }
}
