using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace src.Migrations
{
    public partial class RemoveUniqueFileNameFromBlogEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UniqueNameOfFile",
                table: "BlogEntries");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CareerResponses",
                newName: "id");

            migrationBuilder.AlterColumn<Guid>(
                name: "BlogEntryId",
                table: "BlogEntries",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "CareerResponses",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "BlogEntryId",
                table: "BlogEntries",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "UniqueNameOfFile",
                table: "BlogEntries",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
