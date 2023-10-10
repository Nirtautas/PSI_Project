using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamWebApplication.Migrations
{
    public partial class FullDataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentatorName", "CommentatorSurname", "CourseId", "CreationTime", "UserComment", "UserId" },
                values: new object[,]
                {
                    { 40000, "Jonas", "Paguzinskas", 10000, new DateTime(2023, 10, 10, 15, 35, 6, 793, DateTimeKind.Local).AddTicks(5492), "Sus course", 20000 },
                    { 40001, "Alita", "Stuknaite", 10001, new DateTime(2023, 10, 10, 15, 35, 6, 793, DateTimeKind.Local).AddTicks(5496), "good", 20002 },
                    { 40003, "Alita", "Stuknaite", 10000, new DateTime(2023, 10, 10, 15, 35, 6, 793, DateTimeKind.Local).AddTicks(5499), "Cool", 20002 }
                });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10000,
                column: "CreationDate",
                value: new DateTime(2023, 10, 10, 15, 35, 6, 793, DateTimeKind.Local).AddTicks(2679));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10001,
                column: "CreationDate",
                value: new DateTime(2023, 10, 10, 15, 35, 6, 793, DateTimeKind.Local).AddTicks(2709));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10002,
                column: "CreationDate",
                value: new DateTime(2023, 10, 10, 15, 35, 6, 793, DateTimeKind.Local).AddTicks(2711));

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "CourseId", "CreationDate", "Discriminator", "IsVisible", "LinkContent", "Name" },
                values: new object[] { 30002, 10001, new DateTime(2023, 10, 10, 15, 35, 6, 793, DateTimeKind.Local).AddTicks(5480), "LinkPost", true, "https://www.youtube.com/watch?v=dQw4w9WgXcQ", "Click me" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "CourseId", "CreationDate", "Discriminator", "IsVisible", "Name", "TextContent" },
                values: new object[,]
                {
                    { 30000, 10000, new DateTime(2023, 10, 10, 15, 35, 6, 793, DateTimeKind.Local).AddTicks(5460), "TextPost", true, "Introduction", "This is a placeholder" },
                    { 30001, 10002, new DateTime(2023, 10, 10, 15, 35, 6, 793, DateTimeKind.Local).AddTicks(5470), "TextPost", true, "Knowledge", "This is once more a placeholder" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40000);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40001);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40003);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 30002);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 30000);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 30001);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10000,
                column: "CreationDate",
                value: new DateTime(2023, 10, 10, 13, 50, 42, 200, DateTimeKind.Local).AddTicks(2223));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10001,
                column: "CreationDate",
                value: new DateTime(2023, 10, 10, 13, 50, 42, 200, DateTimeKind.Local).AddTicks(2251));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10002,
                column: "CreationDate",
                value: new DateTime(2023, 10, 10, 13, 50, 42, 200, DateTimeKind.Local).AddTicks(2253));
        }
    }
}
