using AutoMapper;
using src.Data;
using src.Entities;
using src.Models.Dtos;

namespace src.Services
{
    public interface IDisputeRepo
    {
        Task<Dispute> CreateDispute(DisputeForCreationDto complaint, string userId);

        //IEnumerable<Dispute> GetAllDisputes(int pageSize = 10, int pageNumber = 0);

        IEnumerable<DisputeForDisplayForLawyerDto> GetAllDisputesForALawyer(string lawyerEmail, int pageSize = 10, int pageNumber = 0);

        IEnumerable<DisputeForDisplayForCustomerDto> GetAllDisputesForCustomer(string userId, int pageSize = 10, int pageNumber = 0);

        public Dispute GetDisputeByReviewId(string reviewId);

        public DisputeForDisplayForLawyerDto GetDisputeById(string disputeId);
        public DisputeForDisplayForCustomerDto GetDisputeByIdForCustomer(string disputeId);
        public bool UpdateDisputeStatus(string disputeId);
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
            var review = _context.Reviews?.Where(x => x.ReviewId == new Guid(complaint.ReviewId)).FirstOrDefault();
            newDispute.LawyerEmail = review.LawyerEmail;
            newDispute.BadReviewerEmail = review.Email;
            newDispute.Status = 0;
            _context.Disputes.Add(newDispute);
            _context.SaveChanges();
            return newDispute;
        }

        public DisputeForDisplayForLawyerDto GetDisputeById(string disputeId)
        {
           var dispute = _mapper.Map<DisputeForDisplayForLawyerDto>(_context.Disputes.Where(x => x.Id == disputeId).FirstOrDefault());
            return dispute;
        }
        public DisputeForDisplayForCustomerDto GetDisputeByIdForCustomer(string disputeId)
        {
            var dispute = _mapper.Map<DisputeForDisplayForCustomerDto>(_context.Disputes.Where(x => x.Id == disputeId).FirstOrDefault());
            return dispute;
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

        public IEnumerable<DisputeForDisplayForLawyerDto> GetAllDisputesForALawyer(string lawyerEmail, int pageSize = 10, int pageNumber = 0)
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
            var disputes = _mapper.Map<IEnumerable<DisputeForDisplayForLawyerDto>>
                (_context.Disputes.Where(dispute => dispute.LawyerEmail == lawyerEmail)
                .Skip(pageSize * pageNumber).Take(pageSize));

            return disputes;
        }

        public IEnumerable<DisputeForDisplayForCustomerDto> GetAllDisputesForCustomer(string userId, int pageSize = 10, int pageNumber = 0)
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

            var disputesFromRepo = _context.Disputes.Where(dispute => dispute.UserId == userId)
              .Skip(pageSize * pageNumber).Take(pageSize).ToList();
            
            var disputes = _mapper.Map<IEnumerable<DisputeForDisplayForCustomerDto>>(disputesFromRepo);
              
            return disputes;
        }

        public bool UpdateDisputeStatus(string disputeId)
        {
            var dispute =  _context.Disputes.Where(x => x.Id == disputeId).FirstOrDefault();
            dispute.Status = DisputeStatus.Closed;
            _context.SaveChanges();
            return true;

        }
        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}