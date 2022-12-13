using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace src.Migrations
{
    public partial class AddBadReviewerEmailToDisputeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BadReviewerEmail",
                table: "Disputes",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Disputes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2022, 12, 13, 0, 30, 27, 481, DateTimeKind.Local).AddTicks(1752));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2022, 12, 13, 0, 30, 27, 481, DateTimeKind.Local).AddTicks(1770));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2022, 12, 13, 0, 30, 27, 481, DateTimeKind.Local).AddTicks(1772));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2022, 12, 13, 0, 30, 27, 481, DateTimeKind.Local).AddTicks(1773));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2022, 12, 13, 0, 30, 27, 481, DateTimeKind.Local).AddTicks(1821));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2022, 12, 13, 0, 30, 27, 481, DateTimeKind.Local).AddTicks(1825));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreationDate",
                value: new DateTime(2022, 12, 13, 0, 30, 27, 481, DateTimeKind.Local).AddTicks(1826));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreationDate",
                value: new DateTime(2022, 12, 13, 0, 30, 27, 481, DateTimeKind.Local).AddTicks(1827));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreationDate",
                value: new DateTime(2022, 12, 13, 0, 30, 27, 481, DateTimeKind.Local).AddTicks(1827));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreationDate",
                value: new DateTime(2022, 12, 13, 0, 30, 27, 481, DateTimeKind.Local).AddTicks(1829));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreationDate",
                value: new DateTime(2022, 12, 13, 0, 30, 27, 481, DateTimeKind.Local).AddTicks(1830));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreationDate",
                value: new DateTime(2022, 12, 13, 0, 30, 27, 481, DateTimeKind.Local).AddTicks(1831));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreationDate",
                value: new DateTime(2022, 12, 13, 0, 30, 27, 481, DateTimeKind.Local).AddTicks(1832));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreationDate",
                value: new DateTime(2022, 12, 13, 0, 30, 27, 481, DateTimeKind.Local).AddTicks(1833));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BadReviewerEmail",
                table: "Disputes");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Disputes");

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2022, 12, 12, 19, 14, 46, 9, DateTimeKind.Local).AddTicks(7380));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2022, 12, 12, 19, 14, 46, 9, DateTimeKind.Local).AddTicks(7423));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2022, 12, 12, 19, 14, 46, 9, DateTimeKind.Local).AddTicks(7427));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2022, 12, 12, 19, 14, 46, 9, DateTimeKind.Local).AddTicks(7430));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2022, 12, 12, 19, 14, 46, 9, DateTimeKind.Local).AddTicks(7432));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2022, 12, 12, 19, 14, 46, 9, DateTimeKind.Local).AddTicks(7447));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreationDate",
                value: new DateTime(2022, 12, 12, 19, 14, 46, 9, DateTimeKind.Local).AddTicks(7450));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreationDate",
                value: new DateTime(2022, 12, 12, 19, 14, 46, 9, DateTimeKind.Local).AddTicks(7452));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreationDate",
                value: new DateTime(2022, 12, 12, 19, 14, 46, 9, DateTimeKind.Local).AddTicks(7455));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreationDate",
                value: new DateTime(2022, 12, 12, 19, 14, 46, 9, DateTimeKind.Local).AddTicks(7462));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreationDate",
                value: new DateTime(2022, 12, 12, 19, 14, 46, 9, DateTimeKind.Local).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreationDate",
                value: new DateTime(2022, 12, 12, 19, 14, 46, 9, DateTimeKind.Local).AddTicks(7469));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreationDate",
                value: new DateTime(2022, 12, 12, 19, 14, 46, 9, DateTimeKind.Local).AddTicks(7471));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreationDate",
                value: new DateTime(2022, 12, 12, 19, 14, 46, 9, DateTimeKind.Local).AddTicks(7474));
        }
    }
}
