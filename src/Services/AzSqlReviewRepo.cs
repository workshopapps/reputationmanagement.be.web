using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Entities;
using src.Models.Dtos;
using System.Collections;
using System.Linq;

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


        public IEnumerable<Review> GetReviews(int pageNumber, int pageSize)
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
            var reviewsToReturn = _context.Reviews.Skip(pageSize * pageNumber).Take(pageSize) as IEnumerable<Review>;

            return reviewsToReturn;


        }
        public IEnumerable<ReviewForDisplayDto> GetInconclusiveReviews()
        {
            var reviews = _context.Reviews
                .Where(review => review.Status == StatusType.Inconclusive)
                .Select(r => new ReviewForDisplayDto
                {
                    ReviewId = r.ReviewId,
                    Email = r.Email,
                    ReviewString = r.ReviewString,
                    Status = r.Status,
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

        public Review UpdateReviewLawyer(ReviewForUpdateDTO review)
        {
            if (review == null)
            {
                throw new NotImplementedException("No review is passed");
            }
            var reviewToUpdate = _context.Reviews.Where(c => c.ReviewId == review.ReviewId).SingleOrDefault();

            reviewToUpdate.ReviewString = review.ReviewString;
            reviewToUpdate.Status = review.Status;
            reviewToUpdate.UpdatedAt = DateTime.Now;

            Save();

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


        public async Task<UserComplains> PostUserComplains(CreateUserComplainsDto complains)
        {
            var data = new UserComplains()
            {
                ComplaintId = Guid.NewGuid(),
                ComplaintMessage = complains.ComplaintMessage,
                TimeStamp = DateTime.Now,
                UserId = complains.UserId
            };

            var saveData = await _context.UserComplaint.AddAsync(data);
            Save();

            return data;
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

        public void CreateSaveReview(Review review)
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
            _context.SaveChanges();
        }
        /// <summary>
        /// Get the all updated reveiws user, and update the ViewLastTime of the review
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UpdatedRequestDTO>> GetUpdatedReviews(Guid UserId)
        {
            var reviews = await _context.Reviews
                .Where(_x => _x.UserId == UserId && _x.UpdatedAt > _x.CreatedAt && _x.UpdatedAt > _x.ViewLastTime).ToListAsync();
            var r = _mapper.Map<IEnumerable<UpdatedRequestDTO>>(reviews);
            await _context.SaveChangesAsync();
            if (r is null)
            {
                return Enumerable.Empty<UpdatedRequestDTO>();
            }
            return r;


            //var r = new List<UpdatedRequestDTO>();
            //var reviews = await _context.Reviews
            //    .Where(_x => _x.UserId == UserId && _x.UpdatedAt > _x.CreatedAt && _x.UpdatedAt > _x.ViewLastTime).ToListAsync();
            //foreach (var item in reviews)
            //{
            //    var ur = new UpdatedRequestDTO()
            //    {
            //        ReviewId = item.ReviewId,
            //        Email = item.Email,
            //        ReviewString = item.ReviewString,
            //        Status = item.Status,
            //        TimeStamp = item.TimeStamp,
            //        CreatedAt = item.CreatedAt,
            //        UpdatedAt = item.UpdatedAt,
            //    };
            //    r.Add(ur);
            //    item.ViewLastTime = item.UpdatedAt;
            //}
            //await _context.SaveChangesAsync();
            //if (r is null)
            //{
            //    return Enumerable.Empty<UpdatedRequestDTO>();
            //}
            //return r;
        }
    }
}
















