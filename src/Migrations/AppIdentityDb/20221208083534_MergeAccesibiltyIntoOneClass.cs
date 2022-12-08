using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace src.Migrations.AppIdentityDb
{
    public partial class MergeAccesibiltyIntoOneClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "49db1b15-129f-477e-9886-c7b7ca4bc0cd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ab0cd2a-6971-4a96-a4c1-c13d5b6d5f8a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eac65fdd-02be-41ff-950e-292448d73fc2");

            migrationBuilder.AddColumn<bool>(
                name: "HighContrast",
                table: "AspNetUsers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LargeText",
                table: "AspNetUsers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ScreenReader",
                table: "AspNetUsers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "31c11ee6-cd05-4914-9f82-568dc4442ff1", "5604efda-e7ff-43b1-ab53-1764ff69a007", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "41089641-bf5d-45e9-958c-4bb0972c14b3", "648f9ae6-6f2d-49d4-b112-4b4f3a477208", "Lawyer", "LAWYER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a108996c-5046-4de7-9fef-0ad223f36c8b", "5945dcf7-908e-406a-8994-8cc11b7fc15d", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "31c11ee6-cd05-4914-9f82-568dc4442ff1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41089641-bf5d-45e9-958c-4bb0972c14b3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a108996c-5046-4de7-9fef-0ad223f36c8b");

            migrationBuilder.DropColumn(
                name: "HighContrast",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LargeText",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ScreenReader",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "49db1b15-129f-477e-9886-c7b7ca4bc0cd", "086c97f5-77cf-470a-843d-a2d88508cd14", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7ab0cd2a-6971-4a96-a4c1-c13d5b6d5f8a", "df2ae1c6-2149-4fa4-976c-cce2d511ebe0", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "eac65fdd-02be-41ff-950e-292448d73fc2", "91cf3d0f-3109-45ec-bf65-b72676001d18", "Lawyer", "LAWYER" });
        }
    }
}
