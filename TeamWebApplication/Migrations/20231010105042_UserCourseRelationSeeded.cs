using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamWebApplication.Migrations
{
    public partial class UserCourseRelationSeeded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseUser");

            migrationBuilder.CreateTable(
                name: "CoursesUsers",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesUsers", x => new { x.CourseId, x.UserId });
                    table.ForeignKey(
                        name: "FK_CoursesUsers_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursesUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                table: "CoursesUsers",
                columns: new[] { "CourseId", "UserId" },
                values: new object[,]
                {
                    { 10000, 20000 },
                    { 10000, 20001 },
                    { 10001, 20000 },
                    { 10001, 20002 },
                    { 10002, 20001 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoursesUsers_UserId",
                table: "CoursesUsers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoursesUsers");

            migrationBuilder.CreateTable(
                name: "CourseUser",
                columns: table => new
                {
                    CoursesCourseId = table.Column<int>(type: "integer", nullable: false),
                    UsersUserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseUser", x => new { x.CoursesCourseId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_CourseUser_Courses_CoursesCourseId",
                        column: x => x.CoursesCourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseUser_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CourseUser",
                columns: new[] { "CoursesCourseId", "UsersUserId" },
                values: new object[,]
                {
                    { 10000, 20000 },
                    { 10000, 20001 },
                    { 10001, 20000 },
                    { 10001, 20002 },
                    { 10002, 20001 }
                });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10000,
                column: "CreationDate",
                value: new DateTime(2023, 10, 10, 13, 1, 3, 479, DateTimeKind.Local).AddTicks(5396));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10001,
                column: "CreationDate",
                value: new DateTime(2023, 10, 10, 13, 1, 3, 479, DateTimeKind.Local).AddTicks(5422));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 10002,
                column: "CreationDate",
                value: new DateTime(2023, 10, 10, 13, 1, 3, 479, DateTimeKind.Local).AddTicks(5424));

            migrationBuilder.CreateIndex(
                name: "IX_CourseUser_UsersUserId",
                table: "CourseUser",
                column: "UsersUserId");
        }
    }
}
