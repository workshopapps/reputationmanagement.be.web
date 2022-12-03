using src.Entities;
using src.Models.Dtos;

namespace src.Services
{
    public interface IReviewRepository
    {
        public IQueryable<Review> Reviews { get; }

        Review GetReviewById(Guid id);

        IEnumerable<Review> GetReviews(int pageNumber, int pageSize);

        public void CreateSaveReview(Review review);

        public void AddReview(Review review);

        public bool DeleteReview(Guid id);

        public void DeleteReviews(Guid userId);

        public bool Save();

        // Add more CRUD

        IEnumerable<ReviewForDisplayDto> GetInconclusiveReviews();
        
        Review UpdateReviewLawyer( ReviewForUpdateDTO review);

        Task<List<SuccessfulReviewsDto>> GetAllSuccessfulReview();
        IEnumerable<Review> GetReviewByStatusType(StatusType status);
        IEnumerable<Review> GetPendingReview();


        Task<UserComplains> PostUserComplains(CreateUserComplainsDto model);

        ReviewForDisplayDto CreateReview(Review review);
        IEnumerable<Review> GetAllReviews(int pageNumber = 0, int pageSize = 0);
        IEnumerable<Review> GetReviewByPropirity(PriorityType priority);

        Task<IEnumerable<UpdatedRequestDTO>> GetUpdatedReviews(Guid UserId);

        Task<ChallengeReview> PostChallengeReview (ChallengeUserReviewDto challenge);

        public Task<dynamic> ReviewsBulkUpload(IFormFile file);


    }
}
