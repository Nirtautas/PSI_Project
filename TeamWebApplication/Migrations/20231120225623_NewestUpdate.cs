using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class NewestUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40000,
                column: "CreationTime",
                value: new DateTime(2023, 11, 21, 0, 56, 23, 96, DateTimeKind.Local).AddTicks(9385));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40001,
                column: "CreationTime",
                value: new DateTime(2023, 11, 21, 0, 56, 23, 96, DateTimeKind.Local).AddTicks(9390));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40003,
                column: "CreationTime",
                value: new DateTime(2023, 11, 21, 0, 56, 23, 96, DateTimeKind.Local).AddTicks(9394));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10000,
                column: "CreationDate",
                value: new DateTime(2023, 11, 21, 0, 56, 23, 94, DateTimeKind.Local).AddTicks(9144));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10001,
                column: "CreationDate",
                value: new DateTime(2023, 11, 21, 0, 56, 23, 94, DateTimeKind.Local).AddTicks(9187));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10002,
                column: "CreationDate",
                value: new DateTime(2023, 11, 21, 0, 56, 23, 94, DateTimeKind.Local).AddTicks(9189));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 30000,
                column: "CreationDate",
                value: new DateTime(2023, 11, 21, 0, 56, 23, 96, DateTimeKind.Local).AddTicks(9296));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 30001,
                column: "CreationDate",
                value: new DateTime(2023, 11, 21, 0, 56, 23, 96, DateTimeKind.Local).AddTicks(9331));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 30003,
                column: "CreationDate",
                value: new DateTime(2023, 11, 21, 0, 56, 23, 96, DateTimeKind.Local).AddTicks(9355));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40000,
                column: "CreationTime",
                value: new DateTime(2023, 11, 14, 21, 11, 30, 62, DateTimeKind.Local).AddTicks(2875));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40001,
                column: "CreationTime",
                value: new DateTime(2023, 11, 14, 21, 11, 30, 62, DateTimeKind.Local).AddTicks(2880));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40003,
                column: "CreationTime",
                value: new DateTime(2023, 11, 14, 21, 11, 30, 62, DateTimeKind.Local).AddTicks(2884));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10000,
                column: "CreationDate",
                value: new DateTime(2023, 11, 14, 21, 11, 30, 60, DateTimeKind.Local).AddTicks(6566));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10001,
                column: "CreationDate",
                value: new DateTime(2023, 11, 14, 21, 11, 30, 60, DateTimeKind.Local).AddTicks(6600));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10002,
                column: "CreationDate",
                value: new DateTime(2023, 11, 14, 21, 11, 30, 60, DateTimeKind.Local).AddTicks(6602));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 30000,
                column: "CreationDate",
                value: new DateTime(2023, 11, 14, 21, 11, 30, 62, DateTimeKind.Local).AddTicks(2815));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 30001,
                column: "CreationDate",
                value: new DateTime(2023, 11, 14, 21, 11, 30, 62, DateTimeKind.Local).AddTicks(2833));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 30003,
                column: "CreationDate",
                value: new DateTime(2023, 11, 14, 21, 11, 30, 62, DateTimeKind.Local).AddTicks(2858));
        }
    }
}
