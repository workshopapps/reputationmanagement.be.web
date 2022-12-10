using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace src.Migrations
{
    public partial class updateStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2022, 12, 9, 5, 43, 16, 144, DateTimeKind.Local).AddTicks(301));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2022, 12, 9, 5, 43, 16, 144, DateTimeKind.Local).AddTicks(324));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2022, 12, 9, 5, 43, 16, 144, DateTimeKind.Local).AddTicks(326));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2022, 12, 9, 5, 43, 16, 144, DateTimeKind.Local).AddTicks(327));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2022, 12, 9, 5, 43, 16, 144, DateTimeKind.Local).AddTicks(329));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2022, 12, 9, 5, 43, 16, 144, DateTimeKind.Local).AddTicks(332));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreationDate",
                value: new DateTime(2022, 12, 9, 5, 43, 16, 144, DateTimeKind.Local).AddTicks(333));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreationDate",
                value: new DateTime(2022, 12, 9, 5, 43, 16, 144, DateTimeKind.Local).AddTicks(334));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreationDate",
                value: new DateTime(2022, 12, 9, 5, 43, 16, 144, DateTimeKind.Local).AddTicks(335));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreationDate",
                value: new DateTime(2022, 12, 9, 5, 43, 16, 144, DateTimeKind.Local).AddTicks(337));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreationDate",
                value: new DateTime(2022, 12, 9, 5, 43, 16, 144, DateTimeKind.Local).AddTicks(338));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreationDate",
                value: new DateTime(2022, 12, 9, 5, 43, 16, 144, DateTimeKind.Local).AddTicks(339));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreationDate",
                value: new DateTime(2022, 12, 9, 5, 43, 16, 144, DateTimeKind.Local).AddTicks(340));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreationDate",
                value: new DateTime(2022, 12, 9, 5, 43, 16, 144, DateTimeKind.Local).AddTicks(341));
        }
    }
}
