using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeamWebApplication.Migrations
{
    public partial class Complete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "UserDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    loggedInUserId = table.Column<int>(type: "integer", nullable: false),
                    loggedInUserRole = table.Column<int>(type: "integer", nullable: false),
                    currentCourseId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    Faculty = table.Column<int>(type: "integer", nullable: false),
                    Specialization = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false),
                    PostType = table.Column<int>(type: "integer", nullable: false),
                    LinkContent = table.Column<string>(type: "text", nullable: true),
                    TextContent = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Posts_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CommentatorName = table.Column<string>(type: "text", nullable: false),
                    CommentatorSurname = table.Column<string>(type: "text", nullable: false),
                    UserComment = table.Column<string>(type: "text", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseUsers",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseUsers", x => new { x.CourseId, x.UserId });
                    table.ForeignKey(
                        name: "FK_CourseUsers_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "CreationDate", "Description", "IsPublic", "IsVisible", "Name" },
                values: new object[,]
                {
                    { 10000, new DateTime(2023, 10, 12, 22, 47, 1, 136, DateTimeKind.Local).AddTicks(8940), "Course for computer architecture", false, true, "Computer Architecture" },
                    { 10001, new DateTime(2023, 10, 12, 22, 47, 1, 136, DateTimeKind.Local).AddTicks(8970), "Course for functional programming", false, false, "Functional Programming" },
                    { 10002, new DateTime(2023, 10, 12, 22, 47, 1, 136, DateTimeKind.Local).AddTicks(8972), "Course for database systems", true, true, "Database Systems" }
                });

            migrationBuilder.InsertData(
                table: "UserDetails",
                columns: new[] { "Id", "currentCourseId", "loggedInUserId", "loggedInUserRole" },
                values: new object[] { 1, -1, -1, 2 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Faculty", "Name", "Password", "Role", "Specialization", "Surname" },
                values: new object[,]
                {
                    { 20000, "j.paguzinskas@mif.stuf.vu.lt", 0, "Jonas", "qweryty1", 0, 0, "Paguzinskas" },
                    { 20001, "a.jarasunas@mif.stuf.vu.lt", 2, "Armandas", "aludaris17", 0, 4, "Jarasunas" },
                    { 20002, "a.stuknyte@mif.stuf.vu.lt", 0, "Alita", "metupataikau695", 1, 0, "Stuknaite" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentatorName", "CommentatorSurname", "CourseId", "CreationTime", "UserComment", "UserId" },
                values: new object[,]
                {
                    { 40000, "Jonas", "Paguzinskas", 10000, new DateTime(2023, 10, 12, 22, 47, 1, 138, DateTimeKind.Local).AddTicks(1664), "Sus course", 20000 },
                    { 40001, "Alita", "Stuknaite", 10001, new DateTime(2023, 10, 12, 22, 47, 1, 138, DateTimeKind.Local).AddTicks(1668), "good", 20002 },
                    { 40003, "Alita", "Stuknaite", 10000, new DateTime(2023, 10, 12, 22, 47, 1, 138, DateTimeKind.Local).AddTicks(1671), "Cool", 20002 }
                });

            migrationBuilder.InsertData(
                table: "CourseUsers",
                columns: new[] { "CourseId", "UserId" },
                values: new object[,]
                {
                    { 10000, 20000 },
                    { 10000, 20001 },
                    { 10001, 20000 },
                    { 10001, 20002 },
                    { 10002, 20001 }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "CourseId", "CreationDate", "IsVisible", "LinkContent", "Name", "PostType" },
                values: new object[] { 30002, 10001, new DateTime(2023, 10, 12, 22, 47, 1, 138, DateTimeKind.Local).AddTicks(1649), true, "https://www.youtube.com/watch?v=dQw4w9WgXcQ", "Click me", 1 });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "CourseId", "CreationDate", "IsVisible", "Name", "PostType", "TextContent" },
                values: new object[,]
                {
                    { 30000, 10000, new DateTime(2023, 10, 12, 22, 47, 1, 138, DateTimeKind.Local).AddTicks(1611), true, "Introduction", 0, "This is a placeholder" },
                    { 30001, 10002, new DateTime(2023, 10, 12, 22, 47, 1, 138, DateTimeKind.Local).AddTicks(1627), true, "Knowledge", 0, "This is once more a placeholder" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CourseId",
                table: "Comments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseUsers_UserId",
                table: "CourseUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CourseId",
                table: "Posts",
                column: "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "CourseUsers");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "UserDetails");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
