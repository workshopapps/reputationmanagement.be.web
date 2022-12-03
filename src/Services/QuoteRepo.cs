using AutoMapper;
using Microsoft.AspNetCore.Identity;
using src.Data;
using src.Entities;
using src.Models.Dtos;

namespace src.Services
{
    public class QuoteRepo : IQuoteRepository
    {
        public readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public IQueryable<Quote> Quotes => _context.Quotes;

        public QuoteRepo(ApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;      
        }

        public Quote CreateQuote(QuoteForCreationDto quoteForCreation)
        {
            var quote = _mapper.Map<Quote>(quoteForCreation);
            _context.Quotes.Add(quote);
            Save();
            return quote;
        }

        public Quote GetQuoteById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var quote = _context.Quotes.Find(id.ToString());
            return quote;
        }
        
        public void Save()
        {
            _context.SaveChanges();
        }

        public IEnumerable<Quote> GetAll()
        {
            return _context.Quotes.Select(x => x);
        }
    }
}
