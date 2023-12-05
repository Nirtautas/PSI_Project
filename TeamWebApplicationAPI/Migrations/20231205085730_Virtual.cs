using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class Virtual : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40000,
                column: "CreationTime",
                value: new DateTime(2023, 12, 5, 10, 57, 30, 687, DateTimeKind.Local).AddTicks(6812));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40001,
                column: "CreationTime",
                value: new DateTime(2023, 12, 5, 10, 57, 30, 687, DateTimeKind.Local).AddTicks(6844));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40003,
                column: "CreationTime",
                value: new DateTime(2023, 12, 5, 10, 57, 30, 687, DateTimeKind.Local).AddTicks(6847));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10000,
                column: "CreationDate",
                value: new DateTime(2023, 12, 5, 10, 57, 30, 686, DateTimeKind.Local).AddTicks(2204));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10001,
                column: "CreationDate",
                value: new DateTime(2023, 12, 5, 10, 57, 30, 686, DateTimeKind.Local).AddTicks(2234));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10002,
                column: "CreationDate",
                value: new DateTime(2023, 12, 5, 10, 57, 30, 686, DateTimeKind.Local).AddTicks(2236));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 30000,
                column: "CreationDate",
                value: new DateTime(2023, 12, 5, 10, 57, 30, 687, DateTimeKind.Local).AddTicks(6764));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 30001,
                column: "CreationDate",
                value: new DateTime(2023, 12, 5, 10, 57, 30, 687, DateTimeKind.Local).AddTicks(6775));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 30003,
                column: "CreationDate",
                value: new DateTime(2023, 12, 5, 10, 57, 30, 687, DateTimeKind.Local).AddTicks(6794));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40000,
                column: "CreationTime",
                value: new DateTime(2023, 11, 28, 23, 58, 18, 344, DateTimeKind.Local).AddTicks(1871));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40001,
                column: "CreationTime",
                value: new DateTime(2023, 11, 28, 23, 58, 18, 344, DateTimeKind.Local).AddTicks(1874));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40003,
                column: "CreationTime",
                value: new DateTime(2023, 11, 28, 23, 58, 18, 344, DateTimeKind.Local).AddTicks(1877));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10000,
                column: "CreationDate",
                value: new DateTime(2023, 11, 28, 23, 58, 18, 342, DateTimeKind.Local).AddTicks(7203));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10001,
                column: "CreationDate",
                value: new DateTime(2023, 11, 28, 23, 58, 18, 342, DateTimeKind.Local).AddTicks(7231));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10002,
                column: "CreationDate",
                value: new DateTime(2023, 11, 28, 23, 58, 18, 342, DateTimeKind.Local).AddTicks(7233));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 30000,
                column: "CreationDate",
                value: new DateTime(2023, 11, 28, 23, 58, 18, 344, DateTimeKind.Local).AddTicks(1817));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 30001,
                column: "CreationDate",
                value: new DateTime(2023, 11, 28, 23, 58, 18, 344, DateTimeKind.Local).AddTicks(1828));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 30003,
                column: "CreationDate",
                value: new DateTime(2023, 11, 28, 23, 58, 18, 344, DateTimeKind.Local).AddTicks(1851));
        }
    }
}
