using Microsoft.AspNetCore.Mvc;
using src.Entities;
using src.Models.Dtos;

namespace src.Services
{
    public interface IReviewRepository
    {
        public IQueryable<Review> Reviews { get; }

        Review GetReviewById(Guid id);

        IEnumerable<Review> GetReviews(int pageNumber, int pageSize, string? userGuid=null);

        public void CreateSaveReview(Review review);

        public void AddReview(Review review);

        public bool DeleteReview(Guid id);

        public void DeleteReviews(Guid userId);

        public bool Save();

        // Add more CRUD

        IEnumerable<ReviewForDisplayDto> GetInconclusiveReviews();
        
        Review UpdateReviewLawyer( LawyerReviewForUpdateDTO review, Guid reviewId);
        Review UpdateReview(ReviewForUpdateDTO review, Guid reviewId);
        Task<IEnumerable<ReviewForDisplayDto>> GetAllSuccessfulReviews();
        IEnumerable<Review> GetReviewByStatusType(StatusType status);
        IEnumerable<Review> GetPendingReview();

        ReviewForDisplayDto CreateReview(Review review);
        IEnumerable<Review> GetAllReviews(int pageNumber = 0, int pageSize = 0);
        IEnumerable<Review> GetReviewByPropirity(PriorityType priority);

        public Task<IEnumerable<UpdatedRequestDTO>> GetUpdatedReviews(Guid UserId);

        string ClaimReview(Guid reviewId, string email );
        
        IEnumerable<Review> GetClaimedReviews(string email);

        Task<ChallengeReview> PostChallengeReview (ChallengeUserReviewDto challenge);

        public Task<dynamic> ReviewsBulkUpload(IFormFile file);

        public IEnumerable<Review> GetReviewsByBusinessName(string businessName);

        public Task<IEnumerable<Review>> GetReviewsByCustomerEmail(string customerEmail, int pageNumber = 0, int pageSize = 10);
        public Task<int> CountCustomerReviews(string lawyerEmail);

        public Task<IEnumerable<Review>> GetReviewsByLawyerEmail(string lawyerEmail, int pageNumber = 0, int pageSize = 10);
        public Task<int> CountLawyerReviews(string lawyerEmail);

        public Review ReassignReview(Guid reviewId, string lawyerEmail);

        public Review GetFullReview(Guid reviewId);
     
    }
}
