using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Entities;
using src.Models.Dtos;
using System.Collections;

namespace src.Services
{
    public class AzSqlReviewRepo : IReviewRepository
    {
        public readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;


        public IQueryable<Review> Reviews => throw new NotImplementedException();

        public AzSqlReviewRepo(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper;
        }

       
        public void AddReview(Review review)
        {            
            if (review.UserId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(review));
            }

            if (review == null)
            {
                throw new ArgumentNullException(nameof(review));
            }
            _context.Reviews.Add(review);
          
        }
     

        public Review GetReviewById(Guid id)
            {
                if (id == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(id));
                }
            var review = _context.Reviews.Where(c => c.ReviewId == id).SingleOrDefault();
                return review;
            }
        

        public IEnumerable<Review> GetReviews(int pageNumber = 0 , int pageSize = 0)
        {
            var lawyerReviews = _context.Reviews.Select(codedSamurai => new Review()
            {
                ReviewId = codedSamurai.ReviewId,
                Status = codedSamurai.Status,
                ReviewString = codedSamurai.ReviewString,

            }).ToListAsync();
            return (IEnumerable<Review>)lawyerReviews;
        }

        public IEnumerable<ReviewForDisplayDto> GetInconclusiveReviews()
        {
            var reviews = _context.Reviews
                .Where(review => review.Status == StatusType.Inconclusive)
                .Select(r => new ReviewForDisplayDto
                {
                    ReviewId= r.ReviewId,
                    Email= r.Email,
                    ReviewString= r.ReviewString,
                    Status  = r.Status,
                    TimeStamp = r.TimeStamp
                }).ToList();

            if (reviews == null)
            {
                return Enumerable.Empty<ReviewForDisplayDto>();
            }
            return reviews;
        }

        public void DeleteReview(Guid reviewId)
        {
            Review review = GetReviewById(reviewId);
            _context.Reviews.Remove(review);
        }

        /// <summary>
        /// Deletes the reviews associated with a particular user
        /// </summary>
        /// <param name="userId">The particular users Id</param>
        public void DeleteReviews(Guid userId)
        {
            var reviews = _context.Reviews.Where(x => x.UserId == userId).ToList();
            _context.Reviews.RemoveRange(reviews);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose resources when needed
            }
        }

        public Review UpdateReviewLawyer( ReviewForUpdateDTO review)
        {
            if (review == null)
            {
                throw new NotImplementedException("No review is passed");
            }
            var reviewToUpdate = _context.Reviews.Where(c => c.ReviewId == review.ReviewId).SingleOrDefault();

            reviewToUpdate.ReviewString = review.ReviewString;
            reviewToUpdate.Status = review.Status;
            reviewToUpdate.UpdatedAt = DateTime.Now;

            _context.SaveChanges();

            return reviewToUpdate;
        }
        
        public async Task<List<SuccessfulReviewsDto>> GetAllSuccessfulReview()
        {
            var resultModel = new List<SuccessfulReviewsDto>();

            var query = await _context.Reviews
                .Where(x => x.Status == StatusType.Successful)
                .Select(x => new SuccessfulReviewsDto()
                {
                    ReviewId = x.ReviewId,
                    Email = x.Email,
                    Status = x.Status,
                    TimeStamp = x.TimeStamp,
                    Message = x.ReviewString,
                }).ToListAsync();

            if (query != null)
            {
                resultModel = query;
            }

            return resultModel;
        }

        public ReviewForDisplayDto CreateReviews(ReviewForCreationDto review)
        {
            var reviewEntity = _mapper.Map<Review>(review);
            _context.Reviews.Add(reviewEntity);
            var reviewToReturn = _mapper.Map<ReviewForDisplayDto>(reviewEntity);
            _context.SaveChanges();
            return reviewToReturn;
        }

        public IEnumerable<Review> GetAllReviews(int pageNumber = 0, int pageSize = 0)
        {
            if (_context.Reviews == null)
            {
                return Enumerable.Empty<Review>();
            }
            return _context.Reviews;
        }

        public IEnumerable<Review> GetReviewByPropirity(PriorityType priority)
        {
            if (priority == null)
            {
                return Enumerable.Empty<Review>();
            }
            
                return _context.Reviews
                .Where(x => x.Priority.Equals(priority));
        }

    }
}
