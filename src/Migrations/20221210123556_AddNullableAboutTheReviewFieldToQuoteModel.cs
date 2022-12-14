using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace src.Migrations
{
    public partial class AddNullableAboutTheReviewFieldToQuoteModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutTheReview",
                table: "Quotes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2022, 12, 10, 13, 35, 56, 173, DateTimeKind.Local).AddTicks(7520));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2022, 12, 10, 13, 35, 56, 173, DateTimeKind.Local).AddTicks(7544));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2022, 12, 10, 13, 35, 56, 173, DateTimeKind.Local).AddTicks(7546));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2022, 12, 10, 13, 35, 56, 173, DateTimeKind.Local).AddTicks(7547));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2022, 12, 10, 13, 35, 56, 173, DateTimeKind.Local).AddTicks(7548));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2022, 12, 10, 13, 35, 56, 173, DateTimeKind.Local).AddTicks(7553));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreationDate",
                value: new DateTime(2022, 12, 10, 13, 35, 56, 173, DateTimeKind.Local).AddTicks(7554));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreationDate",
                value: new DateTime(2022, 12, 10, 13, 35, 56, 173, DateTimeKind.Local).AddTicks(7555));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreationDate",
                value: new DateTime(2022, 12, 10, 13, 35, 56, 173, DateTimeKind.Local).AddTicks(7557));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreationDate",
                value: new DateTime(2022, 12, 10, 13, 35, 56, 173, DateTimeKind.Local).AddTicks(7559));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreationDate",
                value: new DateTime(2022, 12, 10, 13, 35, 56, 173, DateTimeKind.Local).AddTicks(7560));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreationDate",
                value: new DateTime(2022, 12, 10, 13, 35, 56, 173, DateTimeKind.Local).AddTicks(7561));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreationDate",
                value: new DateTime(2022, 12, 10, 13, 35, 56, 173, DateTimeKind.Local).AddTicks(7562));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreationDate",
                value: new DateTime(2022, 12, 10, 13, 35, 56, 173, DateTimeKind.Local).AddTicks(7563));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutTheReview",
                table: "Quotes");

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2022, 12, 10, 8, 59, 59, 907, DateTimeKind.Local).AddTicks(2012));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2022, 12, 10, 8, 59, 59, 907, DateTimeKind.Local).AddTicks(2033));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2022, 12, 10, 8, 59, 59, 907, DateTimeKind.Local).AddTicks(2035));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2022, 12, 10, 8, 59, 59, 907, DateTimeKind.Local).AddTicks(2098));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2022, 12, 10, 8, 59, 59, 907, DateTimeKind.Local).AddTicks(2100));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2022, 12, 10, 8, 59, 59, 907, DateTimeKind.Local).AddTicks(2104));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreationDate",
                value: new DateTime(2022, 12, 10, 8, 59, 59, 907, DateTimeKind.Local).AddTicks(2106));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreationDate",
                value: new DateTime(2022, 12, 10, 8, 59, 59, 907, DateTimeKind.Local).AddTicks(2107));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreationDate",
                value: new DateTime(2022, 12, 10, 8, 59, 59, 907, DateTimeKind.Local).AddTicks(2108));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreationDate",
                value: new DateTime(2022, 12, 10, 8, 59, 59, 907, DateTimeKind.Local).AddTicks(2110));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreationDate",
                value: new DateTime(2022, 12, 10, 8, 59, 59, 907, DateTimeKind.Local).AddTicks(2112));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreationDate",
                value: new DateTime(2022, 12, 10, 8, 59, 59, 907, DateTimeKind.Local).AddTicks(2113));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreationDate",
                value: new DateTime(2022, 12, 10, 8, 59, 59, 907, DateTimeKind.Local).AddTicks(2114));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreationDate",
                value: new DateTime(2022, 12, 10, 8, 59, 59, 907, DateTimeKind.Local).AddTicks(2116));
        }
    }
}
