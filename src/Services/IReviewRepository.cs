using src.Entities;
using src.Models.Dtos;

namespace src.Services
{
    public interface IReviewRepository
    {
        public IQueryable<Review> Reviews { get; }

        Review GetReviewById(Guid id);

        IEnumerable<Review> GetReviews(int pageNumber, int pageSize);

        public void AddReview(Review review);

        public void DeleteReview(Guid id);

        public void DeleteReviews(Guid userId);

        public bool Save();

        // Add more CRUD

        IEnumerable<ReviewForDisplayDto> GetInconclusiveReviews();
        
        Review UpdateReviewLawyer( ReviewForUpdateDTO review);

        Task<List<SuccessfulReviewsDto>> GetAllSuccessfulReview();
        ReviewForDisplayDto CreateReviews(ReviewForCreationDto review);
        IEnumerable<Review> GetAllReviews(int pageNumber = 0, int pageSize = 0);
        IEnumerable<Review> GetReviewByPropirity(PriorityType priority);
    }
}
