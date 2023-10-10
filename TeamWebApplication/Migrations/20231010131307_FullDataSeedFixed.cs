using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamWebApplication.Migrations
{
    public partial class FullDataSeedFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Posts");

            migrationBuilder.AddColumn<int>(
                name: "PostType",
                table: "Posts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40000,
                column: "CreationTime",
                value: new DateTime(2023, 10, 10, 16, 13, 7, 531, DateTimeKind.Local).AddTicks(2750));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40001,
                column: "CreationTime",
                value: new DateTime(2023, 10, 10, 16, 13, 7, 531, DateTimeKind.Local).AddTicks(2754));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40003,
                column: "CreationTime",
                value: new DateTime(2023, 10, 10, 16, 13, 7, 531, DateTimeKind.Local).AddTicks(2757));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10000,
                column: "CreationDate",
                value: new DateTime(2023, 10, 10, 16, 13, 7, 530, DateTimeKind.Local).AddTicks(11));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10001,
                column: "CreationDate",
                value: new DateTime(2023, 10, 10, 16, 13, 7, 530, DateTimeKind.Local).AddTicks(42));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10002,
                column: "CreationDate",
                value: new DateTime(2023, 10, 10, 16, 13, 7, 530, DateTimeKind.Local).AddTicks(44));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 30002,
                columns: new[] { "CreationDate", "PostType" },
                values: new object[] { new DateTime(2023, 10, 10, 16, 13, 7, 531, DateTimeKind.Local).AddTicks(2733), 1 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 30000,
                column: "CreationDate",
                value: new DateTime(2023, 10, 10, 16, 13, 7, 531, DateTimeKind.Local).AddTicks(2700));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 30001,
                column: "CreationDate",
                value: new DateTime(2023, 10, 10, 16, 13, 7, 531, DateTimeKind.Local).AddTicks(2713));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "PostType",
                table: "Posts");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Posts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40000,
                column: "CreationTime",
                value: new DateTime(2023, 10, 10, 15, 41, 2, 926, DateTimeKind.Local).AddTicks(5561));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40001,
                column: "CreationTime",
                value: new DateTime(2023, 10, 10, 15, 41, 2, 926, DateTimeKind.Local).AddTicks(5565));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40003,
                column: "CreationTime",
                value: new DateTime(2023, 10, 10, 15, 41, 2, 926, DateTimeKind.Local).AddTicks(5568));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10000,
                column: "CreationDate",
                value: new DateTime(2023, 10, 10, 15, 41, 2, 925, DateTimeKind.Local).AddTicks(2375));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10001,
                column: "CreationDate",
                value: new DateTime(2023, 10, 10, 15, 41, 2, 925, DateTimeKind.Local).AddTicks(2415));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10002,
                column: "CreationDate",
                value: new DateTime(2023, 10, 10, 15, 41, 2, 925, DateTimeKind.Local).AddTicks(2417));

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "CourseId", "CreationDate", "Discriminator", "IsVisible", "LinkContent", "Name" },
                values: new object[] { 30002, 10001, new DateTime(2023, 10, 10, 15, 41, 2, 926, DateTimeKind.Local).AddTicks(5514), "LinkPost", true, "https://www.youtube.com/watch?v=dQw4w9WgXcQ", "Click me" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "CourseId", "CreationDate", "Discriminator", "IsVisible", "Name", "TextContent" },
                values: new object[,]
                {
                    { 30000, 10000, new DateTime(2023, 10, 10, 15, 41, 2, 926, DateTimeKind.Local).AddTicks(642), "TextPost", true, "Introduction", "This is a placeholder" },
                    { 30001, 10002, new DateTime(2023, 10, 10, 15, 41, 2, 926, DateTimeKind.Local).AddTicks(655), "TextPost", true, "Knowledge", "This is once more a placeholder" }
                });
        }
    }
}
