using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace src.Migrations.AppIdentityDb
{
    public partial class addlanguagepropertytousermodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afd40973-2030-4306-873b-ecd2d6cc5786");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d8c5b5eb-ec9e-433e-8ae4-86701f56b922");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d97c020d-0df7-40a7-8b3d-0845a605d326");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "AspNetUsers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Language",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "afd40973-2030-4306-873b-ecd2d6cc5786", "eca0a0e2-2d2b-4fb1-925b-abb54d7172f7", "Lawyer", "LAWYER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d8c5b5eb-ec9e-433e-8ae4-86701f56b922", "2173189d-022e-4c32-a28b-3ff9b4f13a96", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d97c020d-0df7-40a7-8b3d-0845a605d326", "0640d8dd-ecc6-41cc-bd34-59caeac51f67", "Administrator", "ADMINISTRATOR" });
        }
    }
}
