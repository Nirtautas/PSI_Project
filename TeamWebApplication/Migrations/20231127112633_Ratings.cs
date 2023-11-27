using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeamWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class Ratings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    UserRating = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => new { x.UserId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_Ratings_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "CourseId", "UserId", "UserRating" },
                values: new object[,]
                {
                    { 10000, 20000, 3 },
                    { 10001, 20000, 5 },
                    { 10000, 20001, 4 },
                    { 10000, 20002, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_CourseId",
                table: "Ratings",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ratings");

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
    }
}
