using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamWebApplication.Migrations
{
    public partial class LinkUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 30002);

            migrationBuilder.DropColumn(
                name: "LinkContent",
                table: "Posts");

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40000,
                column: "CreationTime",
                value: new DateTime(2023, 10, 23, 13, 32, 25, 479, DateTimeKind.Local).AddTicks(5612));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40001,
                column: "CreationTime",
                value: new DateTime(2023, 10, 23, 13, 32, 25, 479, DateTimeKind.Local).AddTicks(5619));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40003,
                column: "CreationTime",
                value: new DateTime(2023, 10, 23, 13, 32, 25, 479, DateTimeKind.Local).AddTicks(5623));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10000,
                column: "CreationDate",
                value: new DateTime(2023, 10, 23, 13, 32, 25, 478, DateTimeKind.Local).AddTicks(3281));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10001,
                column: "CreationDate",
                value: new DateTime(2023, 10, 23, 13, 32, 25, 478, DateTimeKind.Local).AddTicks(3322));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10002,
                column: "CreationDate",
                value: new DateTime(2023, 10, 23, 13, 32, 25, 478, DateTimeKind.Local).AddTicks(3324));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 30000,
                column: "CreationDate",
                value: new DateTime(2023, 10, 23, 13, 32, 25, 479, DateTimeKind.Local).AddTicks(5534));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 30001,
                columns: new[] { "CreationDate", "TextContent" },
                values: new object[] { new DateTime(2023, 10, 23, 13, 32, 25, 479, DateTimeKind.Local).AddTicks(5579), "This is once more a placeholder https://www.youtube.com/watch?v=dQw4w9WgXcQ" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LinkContent",
                table: "Posts",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40000,
                column: "CreationTime",
                value: new DateTime(2023, 10, 18, 17, 3, 58, 877, DateTimeKind.Local).AddTicks(8411));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40001,
                column: "CreationTime",
                value: new DateTime(2023, 10, 18, 17, 3, 58, 877, DateTimeKind.Local).AddTicks(8415));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40003,
                column: "CreationTime",
                value: new DateTime(2023, 10, 18, 17, 3, 58, 877, DateTimeKind.Local).AddTicks(8418));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10000,
                column: "CreationDate",
                value: new DateTime(2023, 10, 18, 17, 3, 58, 876, DateTimeKind.Local).AddTicks(4689));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10001,
                column: "CreationDate",
                value: new DateTime(2023, 10, 18, 17, 3, 58, 876, DateTimeKind.Local).AddTicks(4727));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10002,
                column: "CreationDate",
                value: new DateTime(2023, 10, 18, 17, 3, 58, 876, DateTimeKind.Local).AddTicks(4729));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 30000,
                column: "CreationDate",
                value: new DateTime(2023, 10, 18, 17, 3, 58, 877, DateTimeKind.Local).AddTicks(8358));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 30001,
                columns: new[] { "CreationDate", "TextContent" },
                values: new object[] { new DateTime(2023, 10, 18, 17, 3, 58, 877, DateTimeKind.Local).AddTicks(8375), "This is once more a placeholder" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "CourseId", "CreationDate", "IsVisible", "LinkContent", "Name", "PostType" },
                values: new object[] { 30002, 10001, new DateTime(2023, 10, 18, 17, 3, 58, 877, DateTimeKind.Local).AddTicks(8394), true, "https://www.youtube.com/watch?v=dQw4w9WgXcQ", "Click me", 0 });
        }
    }
}
