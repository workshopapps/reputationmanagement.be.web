using AutoMapper;
using src.Data;
using src.Entities;
using src.Models.Dtos;

namespace src.Services
{
    public interface IDisputeRepo
    {
        Task<Dispute> CreateDispute(DisputeForCreationDto complaint, string userId);

        IEnumerable<Dispute> GetAllDisputes(int pageSize = 10, int pageNumber = 0);

        IEnumerable<Dispute> GetAllDisputesForALawyer(string lawyerEmail, int pageSize = 10, int pageNumber = 0);

        IEnumerable<Dispute> GetAllDisputesForUser(string userId, int pageSize = 10, int pageNumber = 0);

        public Dispute GetDisputeByReviewId(string reviewId);

        public Dispute GetDisputeById(string disputeId);

        public bool Save();
    }

    public class DisputeRepo : IDisputeRepo
    {
        public readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DisputeRepo(ApplicationDbContext context, IMapper mapper, IBufferedFileUploadService bufferedFileUploadService)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Dispute> CreateDispute(DisputeForCreationDto complaint, string userId)
        {
            var newDispute = _mapper.Map<Dispute>(complaint);
            newDispute.Id = Guid.NewGuid().ToString();
            newDispute.UserId = userId;
            newDispute.LawyerEmail =
                _context.Reviews?.Where(x => x.ReviewId == new Guid(complaint.ReviewId)).FirstOrDefault().LawyerEmail;

            _context.Disputes.Add(newDispute);
            _context.SaveChanges();

            return newDispute;
        }

        public Dispute GetDisputeById(string disputeId)
        {
            return _context.Disputes.Where(x => x.Id == disputeId).FirstOrDefault();
        }

        public Dispute GetDisputeByReviewId(string reviewId)
        {
            return _context.Disputes.Where(x => x.ReviewId == reviewId).FirstOrDefault();
        }

        public IEnumerable<Dispute> GetAllDisputes(int pageSize = 10, int pageNumber = 0)
        {
            int defaultPageSize = 10;
            int defaultPageNumber = 0;
            int maxPageSize = 100;

            if (pageSize > maxPageSize || pageSize < 0)
            {
                pageSize = defaultPageSize;
            }
            int availableNumberOfPages = _context.Reviews.Count() / pageSize;
            if (pageNumber > availableNumberOfPages)
            {
                pageNumber = defaultPageNumber;
            }
            return _context.Disputes.Skip(pageSize * pageNumber).Take(pageSize);
        }

        public IEnumerable<Dispute> GetAllDisputesForALawyer(string lawyerEmail, int pageSize = 10, int pageNumber = 0)
        {
            int defaultPageSize = 10;
            int defaultPageNumber = 0;
            int maxPageSize = 100;

            if (pageSize > maxPageSize || pageSize < 0)
            {
                pageSize = defaultPageSize;
            }
            int availableNumberOfPages = _context.Reviews.Count() / pageSize;
            if (pageNumber > availableNumberOfPages)
            {
                pageNumber = defaultPageNumber;
            }
            return _context.Disputes.Where(dispute => dispute.LawyerEmail == lawyerEmail)
                .Skip(pageSize * pageNumber).Take(pageSize);
        }

        public IEnumerable<Dispute> GetAllDisputesForUser(string userId, int pageSize = 10, int pageNumber = 0)
        {
            int defaultPageSize = 10;
            int defaultPageNumber = 0;
            int maxPageSize = 100;

            if (pageSize > maxPageSize || pageSize < 0)
            {
                pageSize = defaultPageSize;
            }
            int availableNumberOfPages = _context.Reviews.Count() / pageSize;
            if (pageNumber > availableNumberOfPages)
            {
                pageNumber = defaultPageNumber;
            }
            return _context.Disputes.Where(dispute => dispute.UserId == userId)
                .Skip(pageSize * pageNumber).Take(pageSize);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}