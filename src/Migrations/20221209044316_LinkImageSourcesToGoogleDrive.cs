using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace src.Migrations
{
    public partial class LinkImageSourcesToGoogleDrive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "PathToImage" },
                values: new object[] { new DateTime(2022, 12, 9, 5, 43, 16, 144, DateTimeKind.Local).AddTicks(301), "https://drive.google.com/uc?export=view&id=1Ay8uRSkkC7t3fesB6Bv9uYPPMTDPoIsg" });

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "PathToImage" },
                values: new object[] { new DateTime(2022, 12, 9, 5, 43, 16, 144, DateTimeKind.Local).AddTicks(324), "https://drive.google.com/uc?export=view&id=1BfStpnrw0gp9KgwhghQY2MauokSpZgmc" });

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreationDate", "PathToImage" },
                values: new object[] { new DateTime(2022, 12, 9, 5, 43, 16, 144, DateTimeKind.Local).AddTicks(326), "https://drive.google.com/uc?export=view&id=17ihd1n1_xPrEPDdQTO2S87ERAxQqkoJQ" });

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreationDate", "PathToImage" },
                values: new object[] { new DateTime(2022, 12, 9, 5, 43, 16, 144, DateTimeKind.Local).AddTicks(327), "https://drive.google.com/uc?export=view&id=1b8srGH7MPKV0kw9apnc5XllJF24OuW0A" });

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreationDate", "PathToImage" },
                values: new object[] { new DateTime(2022, 12, 9, 5, 43, 16, 144, DateTimeKind.Local).AddTicks(329), "https://drive.google.com/uc?export=view&id=1ULJjgPxnEGkLEO9VIX_5m7dkKCjwLayk" });

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreationDate", "PathToImage" },
                values: new object[] { new DateTime(2022, 12, 9, 5, 43, 16, 144, DateTimeKind.Local).AddTicks(332), "https://drive.google.com/uc?export=view&id=1eJQLcMxgObTqbKYc3jujQsvgAd72fQa-" });

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreationDate", "PathToImage" },
                values: new object[] { new DateTime(2022, 12, 9, 5, 43, 16, 144, DateTimeKind.Local).AddTicks(333), "https://drive.google.com/uc?export=view&id=1-r54A5pU0NAVTwr8IQZ9mxvWUG37Zxu2" });

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreationDate", "PathToImage" },
                values: new object[] { new DateTime(2022, 12, 9, 5, 43, 16, 144, DateTimeKind.Local).AddTicks(334), "https://drive.google.com/uc?export=view&id=1YNXXb3_mVJePpK3JPbknuTomNpvuBIAl" });

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreationDate", "PathToImage" },
                values: new object[] { new DateTime(2022, 12, 9, 5, 43, 16, 144, DateTimeKind.Local).AddTicks(335), "https://drive.google.com/uc?export=view&id=1NZY_9xSFlzQ2_OrOThoZlXQUap77k18A" });

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreationDate", "PathToImage" },
                values: new object[] { new DateTime(2022, 12, 9, 5, 43, 16, 144, DateTimeKind.Local).AddTicks(337), "https://drive.google.com/uc?export=view&id=1NZY_9xSFlzQ2_OrOThoZlXQUap77k18A" });

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreationDate", "PathToImage" },
                values: new object[] { new DateTime(2022, 12, 9, 5, 43, 16, 144, DateTimeKind.Local).AddTicks(338), "https://drive.google.com/uc?export=view&id=1YNXXb3_mVJePpK3JPbknuTomNpvuBIAl" });

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreationDate", "PathToImage" },
                values: new object[] { new DateTime(2022, 12, 9, 5, 43, 16, 144, DateTimeKind.Local).AddTicks(339), "https://drive.google.com/uc?export=view&id=1eJQLcMxgObTqbKYc3jujQsvgAd72fQa-" });

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreationDate", "PathToImage" },
                values: new object[] { new DateTime(2022, 12, 9, 5, 43, 16, 144, DateTimeKind.Local).AddTicks(340), "https://drive.google.com/uc?export=view&id=17ihd1n1_xPrEPDdQTO2S87ERAxQqkoJQ" });

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreationDate", "PathToImage" },
                values: new object[] { new DateTime(2022, 12, 9, 5, 43, 16, 144, DateTimeKind.Local).AddTicks(341), "https://drive.google.com/uc?export=view&id=17ihd1n1_xPrEPDdQTO2S87ERAxQqkoJQ" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "PathToImage" },
                values: new object[] { new DateTime(2022, 12, 8, 19, 26, 7, 524, DateTimeKind.Local).AddTicks(7154), "../../assets/images/blog_images/headlines_img1.png}" });

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "PathToImage" },
                values: new object[] { new DateTime(2022, 12, 8, 19, 26, 7, 524, DateTimeKind.Local).AddTicks(7172), "../../assets/images/blog_images/post_img1.png}" });

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreationDate", "PathToImage" },
                values: new object[] { new DateTime(2022, 12, 8, 19, 26, 7, 524, DateTimeKind.Local).AddTicks(7173), "../../assets/images/blog_images/reddit.png" });

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreationDate", "PathToImage" },
                values: new object[] { new DateTime(2022, 12, 8, 19, 26, 7, 524, DateTimeKind.Local).AddTicks(7175), "../../assets/images/blog_images/headlines_img3.png" });

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreationDate", "PathToImage" },
                values: new object[] { new DateTime(2022, 12, 8, 19, 26, 7, 524, DateTimeKind.Local).AddTicks(7176), "../../assets/images/blog_images/privacy.png" });

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreationDate", "PathToImage" },
                values: new object[] { new DateTime(2022, 12, 8, 19, 26, 7, 524, DateTimeKind.Local).AddTicks(7223), "../../assets/images/blog_images/hall.png" });

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreationDate", "PathToImage" },
                values: new object[] { new DateTime(2022, 12, 8, 19, 26, 7, 524, DateTimeKind.Local).AddTicks(7224), "../../assets/images/blog_images/post_img7.png" });

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreationDate", "PathToImage" },
                values: new object[] { new DateTime(2022, 12, 8, 19, 26, 7, 524, DateTimeKind.Local).AddTicks(7225), "../../assets/images/blog_images/headlines_img6.png" });

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreationDate", "PathToImage" },
                values: new object[] { new DateTime(2022, 12, 8, 19, 26, 7, 524, DateTimeKind.Local).AddTicks(7226), "../../assets/images/blog_images/headlines_img7.png" });

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreationDate", "PathToImage" },
                values: new object[] { new DateTime(2022, 12, 8, 19, 26, 7, 524, DateTimeKind.Local).AddTicks(7228), "../../assets/images/blog_images/headlines_img8.png" });

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreationDate", "PathToImage" },
                values: new object[] { new DateTime(2022, 12, 8, 19, 26, 7, 524, DateTimeKind.Local).AddTicks(7229), "../../assets/images/blog_images/headlines_img8.png" });

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreationDate", "PathToImage" },
                values: new object[] { new DateTime(2022, 12, 8, 19, 26, 7, 524, DateTimeKind.Local).AddTicks(7230), "../../assets/images/blog_images/headlines_img8.png" });

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreationDate", "PathToImage" },
                values: new object[] { new DateTime(2022, 12, 8, 19, 26, 7, 524, DateTimeKind.Local).AddTicks(7231), "../../assets/images/blog_images/headlines_img8.png" });

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreationDate", "PathToImage" },
                values: new object[] { new DateTime(2022, 12, 8, 19, 26, 7, 524, DateTimeKind.Local).AddTicks(7232), "../../assets/images/blog_images/headlines_img8.png" });
        }
    }
}
