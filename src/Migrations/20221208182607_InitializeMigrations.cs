using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace src.Migrations
{
    public partial class InitializeMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BlogEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PathToImage = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Url = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tag = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogEntries", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CareerResponses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Position = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Reason = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ResumeFileName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CoverLetterFileName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SubmittedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareerResponses", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "challengeReviews",
                columns: table => new
                {
                    ReviewId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ComplaintMessage = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_challengeReviews", x => x.ReviewId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Quotes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BusinessName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FullName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReviewLocation = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastAccessed = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ViewLastTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ReviewString = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    LawyerEmail = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BusinessType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WebsiteName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    ComplainerName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TimeOfReview = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrderNo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    TrxRef = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserComplaint",
                columns: table => new
                {
                    ComplaintId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ComplaintMessage = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserComplaint", x => x.ComplaintId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "BlogEntries",
                columns: new[] { "Id", "CreationDate", "Description", "PathToImage", "Tag", "Title", "Url" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 12, 8, 19, 26, 7, 524, DateTimeKind.Local).AddTicks(7154), "Defamation removal can be challenging. With our step-by-step guide, learn how to protect ...", "../../assets/images/blog_images/headlines_img1.png}", "Reputation Management", "How to Do Defamation Removal Online", "https://notion.com" },
                    { 2, new DateTime(2022, 12, 8, 19, 26, 7, 524, DateTimeKind.Local).AddTicks(7172), "If you’re well known in your region, the United States, or the world, you’ve likely put a lot of time and resources,,,", "../../assets/images/blog_images/post_img1.png}", "Reputation Management", "Why You Need An Online Reputation", "https://notion.com" },
                    { 3, new DateTime(2022, 12, 8, 19, 26, 7, 524, DateTimeKind.Local).AddTicks(7173), "Fast People Search removal can help protect your privacy online. Learn how to complete the r/reddit.com ...", "../../assets/images/blog_images/reddit.png", "Social Media", "How to Remove Info from Reddit", "https://google.com" },
                    { 4, new DateTime(2022, 12, 8, 19, 26, 7, 524, DateTimeKind.Local).AddTicks(7175), "Since most customers will interact with as business for the first time on the internet, having an effective customer ....", "../../assets/images/blog_images/headlines_img3.png", "Reputation Management", "5 Strategies for Customer Review Management in 2022", "https://google.com" },
                    { 5, new DateTime(2022, 12, 8, 19, 26, 7, 524, DateTimeKind.Local).AddTicks(7176), "Having a great Google review management strategy is very important. They act as an icebreaker to....", "../../assets/images/blog_images/privacy.png", "Google Review", "How to Remove Personal Information from Google", "https://google.com" },
                    { 6, new DateTime(2022, 12, 8, 19, 26, 7, 524, DateTimeKind.Local).AddTicks(7223), "Fast People Search removal can help protect your privacy online. Learn how to complete the FastPeopleSearch...", "../../assets/images/blog_images/hall.png", "Politics", "Reputation Management for Politicians: What to Know", "https://google.com" },
                    { 7, new DateTime(2022, 12, 8, 19, 26, 7, 524, DateTimeKind.Local).AddTicks(7224), "Your public perception is known as your online reputation, telling others who you are, what …", "../../assets/images/blog_images/post_img7.png", "Reputation Management", "How to Remove My Information from Been Verified", "https://google.com" },
                    { 8, new DateTime(2022, 12, 8, 19, 26, 7, 524, DateTimeKind.Local).AddTicks(7225), "If you’re well known in your region, the United States, or the world, you’ve likely put a lot of time and...", "../../assets/images/blog_images/headlines_img6.png", "Reputation Management", "7 Software Development Models to Organize Your Team", "https://google.com" },
                    { 9, new DateTime(2022, 12, 8, 19, 26, 7, 524, DateTimeKind.Local).AddTicks(7226), "Your public perception is known as your online reputation, telling others who you are, what values...", "../../assets/images/blog_images/headlines_img7.png", "Social Media", "How to Remove My Information from Instagram", "https://google.com" },
                    { 10, new DateTime(2022, 12, 8, 19, 26, 7, 524, DateTimeKind.Local).AddTicks(7228), "Defamation removal can be challenging. With our step-by-step guide, learn how to protect ...", "../../assets/images/blog_images/headlines_img8.png", "Reputation Management", "How to Do Defamation Removal Online", "https://google.com" },
                    { 11, new DateTime(2022, 12, 8, 19, 26, 7, 524, DateTimeKind.Local).AddTicks(7229), "Defamation removal can be challenging. With our step-by-step guide, learn how to protect ...", "../../assets/images/blog_images/headlines_img8.png", "Reputation Management", "How to Do Defamation Removal Online", "https://google.com" },
                    { 12, new DateTime(2022, 12, 8, 19, 26, 7, 524, DateTimeKind.Local).AddTicks(7230), "Defamation removal can be challenging. With our step-by-step guide, learn how to protect ...", "../../assets/images/blog_images/headlines_img8.png", "Reputation Management", "How to Do Defamation Removal Online", "https://google.com" },
                    { 13, new DateTime(2022, 12, 8, 19, 26, 7, 524, DateTimeKind.Local).AddTicks(7231), "Defamation removal can be challenging. With our step-by-step guide, learn how to protect ...", "../../assets/images/blog_images/headlines_img8.png", "Reputation Management", "How to Do Defamation Removal Online", "https://google.com" },
                    { 14, new DateTime(2022, 12, 8, 19, 26, 7, 524, DateTimeKind.Local).AddTicks(7232), "Defamation removal can be challenging. With our step-by-step guide, learn how to protect ...", "../../assets/images/blog_images/headlines_img8.png", "Reputation Management", "How to Do Defamation Removal Online", "https://google.com" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogEntries");

            migrationBuilder.DropTable(
                name: "CareerResponses");

            migrationBuilder.DropTable(
                name: "challengeReviews");

            migrationBuilder.DropTable(
                name: "Quotes");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "UserComplaint");
        }
    }
}
