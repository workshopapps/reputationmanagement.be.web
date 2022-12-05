using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace src.Migrations
{
    public partial class AddMorePropertiesToQuoteModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Quotes",
                newName: "Phone");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Quotes",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Quotes");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Quotes",
                newName: "PhoneNumber");
        }
    }
}
