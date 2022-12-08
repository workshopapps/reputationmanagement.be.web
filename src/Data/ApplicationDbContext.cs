using Microsoft.EntityFrameworkCore;
using src.Entities;

namespace src.Data
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<UserComplains> UserComplaint { get; set; }
        public DbSet<ChallengeReview> challengeReviews { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<CareerResponse> CareerResponses { get; set; }
        public DbSet<BlogEntry> BlogEntries { get; set; }

    }
}
