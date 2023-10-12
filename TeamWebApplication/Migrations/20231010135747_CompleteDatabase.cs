using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeamWebApplication.Migrations
{
    public partial class CompleteDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDetails",
                table: "UserDetails");

            migrationBuilder.DeleteData(
                table: "UserDetails",
                keyColumn: "loggedInUserId",
                keyValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "loggedInUserId",
                table: "UserDetails",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserDetails",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDetails",
                table: "UserDetails",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40000,
                column: "CreationTime",
                value: new DateTime(2023, 10, 10, 16, 57, 47, 384, DateTimeKind.Local).AddTicks(3872));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40001,
                column: "CreationTime",
                value: new DateTime(2023, 10, 10, 16, 57, 47, 384, DateTimeKind.Local).AddTicks(3876));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40003,
                column: "CreationTime",
                value: new DateTime(2023, 10, 10, 16, 57, 47, 384, DateTimeKind.Local).AddTicks(3879));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10000,
                column: "CreationDate",
                value: new DateTime(2023, 10, 10, 16, 57, 47, 383, DateTimeKind.Local).AddTicks(1521));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10001,
                column: "CreationDate",
                value: new DateTime(2023, 10, 10, 16, 57, 47, 383, DateTimeKind.Local).AddTicks(1555));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10002,
                column: "CreationDate",
                value: new DateTime(2023, 10, 10, 16, 57, 47, 383, DateTimeKind.Local).AddTicks(1557));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 30002,
                columns: new[] { "CreationDate", "PostType" },
                values: new object[] { new DateTime(2023, 10, 10, 16, 57, 47, 384, DateTimeKind.Local).AddTicks(3857), 1 });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 30000,
                column: "CreationDate",
                value: new DateTime(2023, 10, 10, 16, 57, 47, 384, DateTimeKind.Local).AddTicks(3827));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 30001,
                column: "CreationDate",
                value: new DateTime(2023, 10, 10, 16, 57, 47, 384, DateTimeKind.Local).AddTicks(3838));

            migrationBuilder.InsertData(
                table: "UserDetails",
                columns: new[] { "Id", "currentCourseId", "loggedInUserId", "loggedInUserRole" },
                values: new object[] { 1, -1, -1, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDetails",
                table: "UserDetails");

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 30002);

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserDetails");

            migrationBuilder.AlterColumn<int>(
                name: "loggedInUserId",
                table: "UserDetails",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDetails",
                table: "UserDetails",
                column: "loggedInUserId");

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40000,
                column: "CreationTime",
                value: new DateTime(2023, 10, 10, 16, 36, 11, 497, DateTimeKind.Local).AddTicks(6001));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40001,
                column: "CreationTime",
                value: new DateTime(2023, 10, 10, 16, 36, 11, 497, DateTimeKind.Local).AddTicks(6004));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 40003,
                column: "CreationTime",
                value: new DateTime(2023, 10, 10, 16, 36, 11, 497, DateTimeKind.Local).AddTicks(6008));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10000,
                column: "CreationDate",
                value: new DateTime(2023, 10, 10, 16, 36, 11, 496, DateTimeKind.Local).AddTicks(3502));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10001,
                column: "CreationDate",
                value: new DateTime(2023, 10, 10, 16, 36, 11, 496, DateTimeKind.Local).AddTicks(3532));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10002,
                column: "CreationDate",
                value: new DateTime(2023, 10, 10, 16, 36, 11, 496, DateTimeKind.Local).AddTicks(3534));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 30000,
                column: "CreationDate",
                value: new DateTime(2023, 10, 10, 16, 36, 11, 497, DateTimeKind.Local).AddTicks(5927));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 30001,
                column: "CreationDate",
                value: new DateTime(2023, 10, 10, 16, 36, 11, 497, DateTimeKind.Local).AddTicks(5967));

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "CourseId", "CreationDate", "IsVisible", "LinkContent", "Name", "PostType" },
                values: new object[] { 30002, 10001, new DateTime(2023, 10, 10, 16, 36, 11, 497, DateTimeKind.Local).AddTicks(5987), true, "https://www.youtube.com/watch?v=dQw4w9WgXcQ", "Click me", 0 });
        }
    }
}
