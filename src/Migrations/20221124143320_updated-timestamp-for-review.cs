using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace src.Migrations
{
    public partial class updatedtimestampforreview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "46c7c7ee-b799-4c56-bed7-0bb8ca4f2ce1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5fdc8e8c-c2a9-465b-b5c3-797980cd6b71");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8ddea34-b126-4332-854b-00a8ebc1ff34");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "75b2cdee-7039-4844-959a-3d7811b796ba", "9ff4a6e7-44a2-4351-9e20-9189c3682bc2", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bb531a0c-bd01-482e-a71d-7fe3fef77b80", "6ec510ce-03c9-472b-ab6e-7d62dc5e0375", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bbff654b-c3a2-458c-afe9-ce05b02b57b7", "7092c399-4f26-4159-95eb-203b3b1ed2b8", "Lawyer", "LAWYER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "75b2cdee-7039-4844-959a-3d7811b796ba");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bb531a0c-bd01-482e-a71d-7fe3fef77b80");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bbff654b-c3a2-458c-afe9-ce05b02b57b7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "46c7c7ee-b799-4c56-bed7-0bb8ca4f2ce1", "c5b35244-1c61-440d-9527-7430c4b1e295", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5fdc8e8c-c2a9-465b-b5c3-797980cd6b71", "132f5ae0-bb17-4bcd-9908-b652cfd4369a", "Lawyer", "LAWYER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f8ddea34-b126-4332-854b-00a8ebc1ff34", "0525062a-24bc-4125-bdad-083a8e424452", "Administrator", "ADMINISTRATOR" });
        }
    }
}
