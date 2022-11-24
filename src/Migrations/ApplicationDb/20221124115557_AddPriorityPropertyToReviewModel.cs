using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace src.Migrations.ApplicationDb
{
    public partial class AddPriorityPropertyToReviewModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Reviews");
        }
    }
}
