using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace src.Migrations
{
    public partial class UpdateStatusType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2022, 12, 11, 13, 19, 15, 381, DateTimeKind.Local).AddTicks(6927));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2022, 12, 11, 13, 19, 15, 381, DateTimeKind.Local).AddTicks(6950));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2022, 12, 11, 13, 19, 15, 381, DateTimeKind.Local).AddTicks(6953));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2022, 12, 11, 13, 19, 15, 381, DateTimeKind.Local).AddTicks(6954));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2022, 12, 11, 13, 19, 15, 381, DateTimeKind.Local).AddTicks(6956));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2022, 12, 11, 13, 19, 15, 381, DateTimeKind.Local).AddTicks(6960));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreationDate",
                value: new DateTime(2022, 12, 11, 13, 19, 15, 381, DateTimeKind.Local).AddTicks(6962));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreationDate",
                value: new DateTime(2022, 12, 11, 13, 19, 15, 381, DateTimeKind.Local).AddTicks(6963));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreationDate",
                value: new DateTime(2022, 12, 11, 13, 19, 15, 381, DateTimeKind.Local).AddTicks(6965));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreationDate",
                value: new DateTime(2022, 12, 11, 13, 19, 15, 381, DateTimeKind.Local).AddTicks(6967));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreationDate",
                value: new DateTime(2022, 12, 11, 13, 19, 15, 381, DateTimeKind.Local).AddTicks(6969));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreationDate",
                value: new DateTime(2022, 12, 11, 13, 19, 15, 381, DateTimeKind.Local).AddTicks(6970));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreationDate",
                value: new DateTime(2022, 12, 11, 13, 19, 15, 381, DateTimeKind.Local).AddTicks(6972));

            migrationBuilder.UpdateData(
                table: "BlogEntries",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreationDate",
                value: new DateTime(2022, 12, 11, 13, 19, 15, 381, DateTimeKind.Local).AddTicks(6973));
        }
    }
}
