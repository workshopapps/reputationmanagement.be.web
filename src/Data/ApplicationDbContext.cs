using Microsoft.EntityFrameworkCore;
using src.Entities;

namespace src.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogEntry>().HasData(
                    new List<BlogEntry>(){
                    new BlogEntry
                    {
                        Id = 1,
                        PathToImage = "../../assets/images/blog_images/headlines_img1.png}",
                        Title = "How to Do Defamation Removal Online",
                        Description = "Defamation removal can be challenging. With our step-by-step guide, learn how to protect ...",
                        Url = "https://notion.com",
                        Tag = "Reputation Management"
                    },
                    new BlogEntry
                    {
                        Id = 2,
                        PathToImage = "../../assets/images/blog_images/post_img1.png}",
                        Title = "Why You Need An Online Reputation",
                        Description = "If you’re well known in your region, the United States, or the world, you’ve likely put a lot of time and resources,,,",
                        Url = "https://notion.com",
                        Tag = "Reputation Management"
                    },
                    new BlogEntry
                    {
                        Id = 3,
                        PathToImage = "../../assets/images/blog_images/reddit.png",
                        Title = "How to Remove Info from Reddit",
                        Description = "Fast People Search removal can help protect your privacy online. Learn how to complete the r/reddit.com ...",
                        Url = "https://google.com",
                        Tag = "Social Media"
                    },
                    new BlogEntry
                    {
                        Id = 4,
                        PathToImage = "../../assets/images/blog_images/headlines_img3.png",
                        Title = "5 Strategies for Customer Review Management in 2022",
                        Description = "Since most customers will interact with as business for the first time on the internet, having an effective customer ....",
                        Url = "https://google.com",
                        Tag = "Reputation Management"
                    },
                    new BlogEntry
                    {
                        Id = 5,
                        PathToImage = "../../assets/images/blog_images/privacy.png",
                        Title = "How to Remove Personal Information from Google",
                        Description = "Having a great Google review management strategy is very important. They act as an icebreaker to....",
                        Url = "https://google.com",
                        Tag = "Google Review"
                    },
                    new BlogEntry
                    {
                        Id = 6,
                        PathToImage = "../../assets/images/blog_images/hall.png",
                        Title = "Reputation Management for Politicians: What to Know",
                        Description = "Fast People Search removal can help protect your privacy online. Learn how to complete the FastPeopleSearch...",
                        Url = "https://google.com",
                        Tag = "Politics"
                    },
                    new BlogEntry
                    {
                        Id = 7,
                        PathToImage = "../../assets/images/blog_images/post_img7.png",
                        Title = "How to Remove My Information from Been Verified",
                        Description = "Your public perception is known as your online reputation, telling others who you are, what …",
                        Url = "https://google.com",
                        Tag = "Reputation Management"
                    },
                    new BlogEntry
                    {
                        Id = 8,
                        PathToImage = "../../assets/images/blog_images/headlines_img6.png",
                        Title = "7 Software Development Models to Organize Your Team",
                        Description = "If you’re well known in your region, the United States, or the world, you’ve likely put a lot of time and...",
                        Url = "https://google.com",
                        Tag = "Reputation Management"
                    },
                    new BlogEntry
                    {
                        Id = 9,
                        PathToImage = "../../assets/images/blog_images/headlines_img7.png",
                        Title = "How to Remove My Information from Instagram",
                        Description = "Your public perception is known as your online reputation, telling others who you are, what values...",
                        Url = "https://google.com",
                        Tag = "Social Media"
                    },
                    new BlogEntry
                    {
                        Id = 10,
                        PathToImage = "../../assets/images/blog_images/headlines_img8.png",
                        Title = "How to Do Defamation Removal Online",
                        Description = "Defamation removal can be challenging. With our step-by-step guide, learn how to protect ...",
                        Url = "https://google.com",
                        Tag = "Reputation Management"
                    },
                    new BlogEntry
                    {
                        Id = 11,
                        PathToImage = "../../assets/images/blog_images/headlines_img8.png",
                        Title = "How to Do Defamation Removal Online",
                        Description = "Defamation removal can be challenging. With our step-by-step guide, learn how to protect ...",
                        Url = "https://google.com",
                        Tag = "Reputation Management"
                    },
                    new BlogEntry
                    {
                        Id = 12,
                        PathToImage = "../../assets/images/blog_images/headlines_img8.png",
                        Title = "How to Do Defamation Removal Online",
                        Description = "Defamation removal can be challenging. With our step-by-step guide, learn how to protect ...",
                        Url = "https://google.com",
                        Tag = "Reputation Management"
                    },
                    new BlogEntry
                    {
                        Id = 13,
                        PathToImage = "../../assets/images/blog_images/headlines_img8.png",
                        Title = "How to Do Defamation Removal Online",
                        Description = "Defamation removal can be challenging. With our step-by-step guide, learn how to protect ...",
                        Url = "https://google.com",
                        Tag = "Reputation Management"
                    },
                    new BlogEntry
                    {
                        Id = 14,
                        PathToImage = "../../assets/images/blog_images/headlines_img8.png",
                        Title = "How to Do Defamation Removal Online",
                        Description = "Defamation removal can be challenging. With our step-by-step guide, learn how to protect ...",
                        Url = "https://google.com",
                        Tag = "Reputation Management"
                    },
                    });
        }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<UserComplains> UserComplaint { get; set; }
        public DbSet<ChallengeReview> challengeReviews { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<CareerResponse> CareerResponses { get; set; }
        public DbSet<BlogEntry> BlogEntries { get; set; }
    }
}