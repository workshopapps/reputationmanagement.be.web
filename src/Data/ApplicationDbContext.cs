using Microsoft.EntityFrameworkCore;
using src.Entities;

namespace src.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //        var imagePathsInImageUploadFolder = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, "Uploads", "BlogImageUpload"));
        //        foreach (var imagePath in imagePathsInImageUploadFolder)
        //        {
        //            modelBuilder.Entity<BlogEntry>().HasData(
        //                    new BlogEntry
        //                    {
        //                        PathToImage = imagePath,
        //                        Title = "Sample Title",
        //                        Description = "A black/white painting of a monkey",
        //                        CreationDate = DateTime.Now,
        //                        Url = "https://notion.com"
        //                    });
        //        }
            
        //}

        public DbSet<Review> Reviews { get; set; }
        public DbSet<UserComplains> UserComplaint { get; set; }
        public DbSet<ChallengeReview> challengeReviews { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<CareerResponse> CareerResponses { get; set; }
        public DbSet<BlogEntry> BlogEntries { get; set; }
    }
}