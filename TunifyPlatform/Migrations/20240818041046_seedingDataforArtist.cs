using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TunifyPlatform.Migrations
{
    /// <inheritdoc />
    public partial class seedingDataforArtist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "Artist 4" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "JoinDate",
                value: new DateTime(2024, 8, 18, 7, 10, 45, 944, DateTimeKind.Local).AddTicks(7032));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "JoinDate",
                value: new DateTime(2024, 8, 8, 7, 10, 45, 944, DateTimeKind.Local).AddTicks(7045));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                column: "JoinDate",
                value: new DateTime(2024, 7, 29, 7, 10, 45, 944, DateTimeKind.Local).AddTicks(7051));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                column: "JoinDate",
                value: new DateTime(2024, 7, 19, 7, 10, 45, 944, DateTimeKind.Local).AddTicks(7053));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5,
                column: "JoinDate",
                value: new DateTime(2024, 7, 9, 7, 10, 45, 944, DateTimeKind.Local).AddTicks(7054));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: 4);

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
    }
}
