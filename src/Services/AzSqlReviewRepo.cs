using AutoMapper;
using CsvHelper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Entities;
using src.Models.Dtos;
using System.Collections;
using System.Net;
using System.Security.Claims;
using System.Formats.Asn1;

namespace src.Services
{
    public class AzSqlReviewRepo : IReviewRepository
    {
        public readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;



        public IQueryable<Review> Reviews => throw new NotImplementedException();

        public AzSqlReviewRepo(ApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper;
            _userManager = userManager;
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

        public bool DeleteReview(Guid reviewId)
        {
            Review review = GetReviewById(reviewId);
            if (review == null)
            {
                return false;
            }
            _context.Reviews.Remove(review);
            return true;
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

        public IEnumerable<Review> GetReviewByStatusType(StatusType status)
        {
            return _context.Reviews
            .Where(s => s.Status.Equals(status));
        }

        public IEnumerable<Review> GetPendingReview()
        {
            return _context.Reviews
            .Where(p => p.Status == StatusType.PendingReview);
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

        public ReviewForDisplayDto CreateReview(Review review)
        {
            
            _context.Reviews.Add(review);
            var reviewToReturn = _mapper.Map<ReviewForDisplayDto>(review);
            Save();
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

        public async Task<IEnumerable<UpdatedRequestDTO>> GetUpdatedReviews(Guid UserId)
        {
            var reviews = await _context.Reviews
                .Where(_x => _x.UserId == UserId && _x.UpdatedAt > _x.CreatedAt)
                .Select(r => new UpdatedRequestDTO
                {
                    ReviewId = r.ReviewId,
                    Email = r.Email,
                    ReviewString = r.ReviewString,
                    Status = r.Status,
                    TimeStamp = r.TimeStamp,
                }).ToListAsync();

            if (reviews == null)
            {
                return Enumerable.Empty<UpdatedRequestDTO>();
            }
            return reviews;
        }

        public string ClaimReview(Guid reviewId, string email)
        {
            //Get the ID of the signed in lawyer
            // var userId = SignInManager.AuthenticationManager.AuthenticationResponseGrant.Identity.GetUserId();

            var review = _context.Reviews.FirstOrDefault(c => c.ReviewId == reviewId);
            if(review == null)
            {
                return null;
                
            }

            if(review.LawyerEmail != null)
            {
               return null;
            }
            
            review.LawyerEmail = email;
            _context.SaveChanges();
            return review.LawyerEmail;
              
        }

        public IEnumerable<Review> GetClaimedReviews(string email)
        {
            var result = _context.Reviews.Where(x => x.LawyerEmail == email).ToList();
            return result;
        }

        public async Task<ChallengeReview> PostChallengeReview (ChallengeUserReviewDto challenge)
        {
            var model = new ChallengeReview()
            {
                ReviewId = challenge.ReviewId,
                UserId = challenge.UserId,
                ComplaintMessage = challenge.ComplaintMessage
            };

            var savemodel = await _context.challengeReviews.AddAsync(model);
            Save();

            return model;
        }

        public async Task<dynamic> ReviewsBulkUpload(IFormFile file)
        {

            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            using var memoryStream = new MemoryStream(new byte[file.Length]);
            file.CopyTo(memoryStream);
            memoryStream.Position = 0;

            using (var reader = new StreamReader(memoryStream))
            using (var csvReader = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
            {
                var csvRecords = csvReader.GetRecords<ReviewCsvDto>().ToList();

                foreach (var item in csvRecords)
                {
                    var user = _userManager.FindByEmailAsync(item.CustomerEmail).GetAwaiter().GetResult();
                    var userId = Guid.Parse(user.Id);

                    _context.Reviews.Add(new Review
                    {
                        Email = item.Email,
                        ReviewString = item.ReviewString,
                        Priority = Enum.Parse<PriorityType>(item.Priority),
                        Status = Enum.Parse<StatusType>(item.Status),
                        TimeStamp = DateTime.Now,
                        UserId = userId
                    });
                }
                _context.SaveChanges();
                return "Success";
            }
        }
    }
}
















