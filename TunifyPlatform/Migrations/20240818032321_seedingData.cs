using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TunifyPlatform.Migrations
{
    /// <inheritdoc />
    public partial class seedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "JoinDate",
                value: new DateTime(2024, 8, 18, 6, 23, 21, 764, DateTimeKind.Local).AddTicks(9803));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "JoinDate",
                value: new DateTime(2024, 8, 8, 6, 23, 21, 764, DateTimeKind.Local).AddTicks(9815));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                column: "JoinDate",
                value: new DateTime(2024, 7, 29, 6, 23, 21, 764, DateTimeKind.Local).AddTicks(9822));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                column: "JoinDate",
                value: new DateTime(2024, 7, 19, 6, 23, 21, 764, DateTimeKind.Local).AddTicks(9823));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5,
                column: "JoinDate",
                value: new DateTime(2024, 7, 9, 6, 23, 21, 764, DateTimeKind.Local).AddTicks(9825));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "JoinDate",
                value: new DateTime(2024, 8, 16, 9, 57, 18, 214, DateTimeKind.Local).AddTicks(5822));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "JoinDate",
                value: new DateTime(2024, 8, 6, 9, 57, 18, 214, DateTimeKind.Local).AddTicks(5836));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                column: "JoinDate",
                value: new DateTime(2024, 7, 27, 9, 57, 18, 214, DateTimeKind.Local).AddTicks(5843));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                column: "JoinDate",
                value: new DateTime(2024, 7, 17, 9, 57, 18, 214, DateTimeKind.Local).AddTicks(5844));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5,
                column: "JoinDate",
                value: new DateTime(2024, 7, 7, 9, 57, 18, 214, DateTimeKind.Local).AddTicks(5845));
        }
    }
}
