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
                        PathToImage = "https://drive.google.com/uc?export=view&id=1Ay8uRSkkC7t3fesB6Bv9uYPPMTDPoIsg",
                        Title = "How to Do Defamation Removal Online",
                        Description = "Defamation removal can be challenging. With our step-by-step guide, learn how to protect ...",
                        Url = "https://notion.com",
                        Tag = "Reputation Management"
                    },
                    new BlogEntry
                    {
                        Id = 2,
                        PathToImage = "https://drive.google.com/uc?export=view&id=1BfStpnrw0gp9KgwhghQY2MauokSpZgmc",
                        Title = "Why You Need An Online Reputation",
                        Description = "If you’re well known in your region, the United States, or the world, you’ve likely put a lot of time and resources,,,",
                        Url = "https://notion.com",
                        Tag = "Reputation Management"
                    },
                    new BlogEntry
                    {
                        Id = 3,
                        PathToImage = "https://drive.google.com/uc?export=view&id=17ihd1n1_xPrEPDdQTO2S87ERAxQqkoJQ",
                        Title = "How to Remove Info from Reddit",
                        Description = "Fast People Search removal can help protect your privacy online. Learn how to complete the r/reddit.com ...",
                        Url = "https://google.com",
                        Tag = "Social Media"
                    },
                    new BlogEntry
                    {
                        Id = 4,
                        PathToImage = "https://drive.google.com/uc?export=view&id=1b8srGH7MPKV0kw9apnc5XllJF24OuW0A",
                        Title = "5 Strategies for Customer Review Management in 2022",
                        Description = "Since most customers will interact with as business for the first time on the internet, having an effective customer ....",
                        Url = "https://google.com",
                        Tag = "Reputation Management"
                    },
                    new BlogEntry
                    {
                        Id = 5,
                        PathToImage = "https://drive.google.com/uc?export=view&id=1ULJjgPxnEGkLEO9VIX_5m7dkKCjwLayk",
                        Title = "How to Remove Personal Information from Google",
                        Description = "Having a great Google review management strategy is very important. They act as an icebreaker to....",
                        Url = "https://google.com",
                        Tag = "Google Review"
                    },
                    new BlogEntry
                    {
                        Id = 6,
                        PathToImage = "https://drive.google.com/uc?export=view&id=1eJQLcMxgObTqbKYc3jujQsvgAd72fQa-",
                        Title = "Reputation Management for Politicians: What to Know",
                        Description = "Fast People Search removal can help protect your privacy online. Learn how to complete the FastPeopleSearch...",
                        Url = "https://google.com",
                        Tag = "Politics"
                    },
                    new BlogEntry
                    {
                        Id = 7,
                        PathToImage = "https://drive.google.com/uc?export=view&id=1-r54A5pU0NAVTwr8IQZ9mxvWUG37Zxu2",
                        Title = "How to Remove My Information from Been Verified",
                        Description = "Your public perception is known as your online reputation, telling others who you are, what …",
                        Url = "https://google.com",
                        Tag = "Reputation Management"
                    },
                    new BlogEntry
                    {
                        Id = 8,
                        PathToImage = "https://drive.google.com/uc?export=view&id=1YNXXb3_mVJePpK3JPbknuTomNpvuBIAl",
                        Title = "7 Software Development Models to Organize Your Team",
                        Description = "If you’re well known in your region, the United States, or the world, you’ve likely put a lot of time and...",
                        Url = "https://google.com",
                        Tag = "Reputation Management"
                    },
                    new BlogEntry
                    {
                        Id = 9,
                        PathToImage = "https://drive.google.com/uc?export=view&id=1NZY_9xSFlzQ2_OrOThoZlXQUap77k18A",
                        Title = "How to Remove My Information from Instagram",
                        Description = "Your public perception is known as your online reputation, telling others who you are, what values...",
                        Url = "https://google.com",
                        Tag = "Social Media"
                    },
                    new BlogEntry
                    {
                        Id = 10,
                        PathToImage = "https://drive.google.com/uc?export=view&id=1NZY_9xSFlzQ2_OrOThoZlXQUap77k18A",
                        Title = "How to Do Defamation Removal Online",
                        Description = "Defamation removal can be challenging. With our step-by-step guide, learn how to protect ...",
                        Url = "https://google.com",
                        Tag = "Reputation Management"
                    },
                    new BlogEntry
                    {
                        Id = 11,
                        PathToImage = "https://drive.google.com/uc?export=view&id=1YNXXb3_mVJePpK3JPbknuTomNpvuBIAl",
                        Title = "How to Do Defamation Removal Online",
                        Description = "Defamation removal can be challenging. With our step-by-step guide, learn how to protect ...",
                        Url = "https://google.com",
                        Tag = "Reputation Management"
                    },
                    new BlogEntry
                    {
                        Id = 12,
                        PathToImage = "https://drive.google.com/uc?export=view&id=1eJQLcMxgObTqbKYc3jujQsvgAd72fQa-",
                        Title = "How to Do Defamation Removal Online",
                        Description = "Defamation removal can be challenging. With our step-by-step guide, learn how to protect ...",
                        Url = "https://google.com",
                        Tag = "Reputation Management"
                    },
                    new BlogEntry
                    {
                        Id = 13,
                        PathToImage = "https://drive.google.com/uc?export=view&id=17ihd1n1_xPrEPDdQTO2S87ERAxQqkoJQ",
                        Title = "How to Do Defamation Removal Online",
                        Description = "Defamation removal can be challenging. With our step-by-step guide, learn how to protect ...",
                        Url = "https://google.com",
                        Tag = "Reputation Management"
                    },
                    new BlogEntry
                    {
                        Id = 14,
                        PathToImage = "https://drive.google.com/uc?export=view&id=17ihd1n1_xPrEPDdQTO2S87ERAxQqkoJQ",
                        Title = "How to Do Defamation Removal Online",
                        Description = "Defamation removal can be challenging. With our step-by-step guide, learn how to protect ...",
                        Url = "https://google.com",
                        Tag = "Reputation Management"
                    },
                    });
        }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<Dispute> Disputes { get; set; }
        public DbSet<ChallengeReview> challengeReviews { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<CareerResponse> CareerResponses { get; set; }
        public DbSet<BlogEntry> BlogEntries { get; set; }
        public DbSet<ContactUs> ContactUs {get; set;}
    }
}