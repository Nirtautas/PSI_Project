using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class RatingDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "UserRating",
                table: "Ratings",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

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

            migrationBuilder.UpdateData(
                table: "Ratings",
                keyColumns: new[] { "CourseId", "UserId" },
                keyValues: new object[] { 10000, 20000 },
                column: "UserRating",
                value: 3m);

            migrationBuilder.UpdateData(
                table: "Ratings",
                keyColumns: new[] { "CourseId", "UserId" },
                keyValues: new object[] { 10001, 20000 },
                column: "UserRating",
                value: 5m);

            migrationBuilder.UpdateData(
                table: "Ratings",
                keyColumns: new[] { "CourseId", "UserId" },
                keyValues: new object[] { 10000, 20001 },
                column: "UserRating",
                value: 4m);

            migrationBuilder.UpdateData(
                table: "Ratings",
                keyColumns: new[] { "CourseId", "UserId" },
                keyValues: new object[] { 10000, 20002 },
                column: "UserRating",
                value: 3m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserRating",
                table: "Ratings",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40000,
                column: "CreationTime",
                value: new DateTime(2023, 11, 27, 13, 26, 33, 288, DateTimeKind.Local).AddTicks(1724));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40001,
                column: "CreationTime",
                value: new DateTime(2023, 11, 27, 13, 26, 33, 288, DateTimeKind.Local).AddTicks(1728));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40003,
                column: "CreationTime",
                value: new DateTime(2023, 11, 27, 13, 26, 33, 288, DateTimeKind.Local).AddTicks(1730));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10000,
                column: "CreationDate",
                value: new DateTime(2023, 11, 27, 13, 26, 33, 286, DateTimeKind.Local).AddTicks(6651));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10001,
                column: "CreationDate",
                value: new DateTime(2023, 11, 27, 13, 26, 33, 286, DateTimeKind.Local).AddTicks(6682));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10002,
                column: "CreationDate",
                value: new DateTime(2023, 11, 27, 13, 26, 33, 286, DateTimeKind.Local).AddTicks(6684));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 30000,
                column: "CreationDate",
                value: new DateTime(2023, 11, 27, 13, 26, 33, 288, DateTimeKind.Local).AddTicks(1669));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 30001,
                column: "CreationDate",
                value: new DateTime(2023, 11, 27, 13, 26, 33, 288, DateTimeKind.Local).AddTicks(1684));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 30003,
                column: "CreationDate",
                value: new DateTime(2023, 11, 27, 13, 26, 33, 288, DateTimeKind.Local).AddTicks(1706));

            migrationBuilder.UpdateData(
                table: "Ratings",
                keyColumns: new[] { "CourseId", "UserId" },
                keyValues: new object[] { 10000, 20000 },
                column: "UserRating",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Ratings",
                keyColumns: new[] { "CourseId", "UserId" },
                keyValues: new object[] { 10001, 20000 },
                column: "UserRating",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Ratings",
                keyColumns: new[] { "CourseId", "UserId" },
                keyValues: new object[] { 10000, 20001 },
                column: "UserRating",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Ratings",
                keyColumns: new[] { "CourseId", "UserId" },
                keyValues: new object[] { 10000, 20002 },
                column: "UserRating",
                value: 3);
        }
    }
}
