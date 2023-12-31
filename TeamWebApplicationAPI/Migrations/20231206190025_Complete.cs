﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeamWebApplicationAPI.Migrations
{
    /// <inheritdoc />
    public partial class Complete : Migration
    {
        /// <inheritdoc />
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
                    FileName = table.Column<string>(type: "text", nullable: true),
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
                    CommentatorName = table.Column<string>(type: "text", nullable: true),
                    CommentatorSurname = table.Column<string>(type: "text", nullable: true),
                    UserComment = table.Column<string>(type: "text", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    UserRating = table.Column<decimal>(type: "numeric", nullable: false)
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

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "CreationDate", "Description", "IsPublic", "IsVisible", "Name" },
                values: new object[,]
                {
                    { 10000, new DateTime(2023, 12, 6, 21, 0, 25, 190, DateTimeKind.Local).AddTicks(8834), "Course for computer architecture", false, true, "Computer Architecture" },
                    { 10001, new DateTime(2023, 12, 6, 21, 0, 25, 190, DateTimeKind.Local).AddTicks(8865), "Course for functional programming", false, false, "Functional Programming" },
                    { 10002, new DateTime(2023, 12, 6, 21, 0, 25, 190, DateTimeKind.Local).AddTicks(8867), "Course for database systems", true, true, "Database Systems" }
                });

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
                    { 40000, "Jonas", "Paguzinskas", 10000, new DateTime(2023, 12, 6, 21, 0, 25, 192, DateTimeKind.Local).AddTicks(3691), "Sus course", 20000 },
                    { 40001, "Alita", "Stuknaite", 10001, new DateTime(2023, 12, 6, 21, 0, 25, 192, DateTimeKind.Local).AddTicks(3694), "good", 20002 },
                    { 40003, "Alita", "Stuknaite", 10000, new DateTime(2023, 12, 6, 21, 0, 25, 192, DateTimeKind.Local).AddTicks(3697), "Cool", 20002 }
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
                columns: new[] { "PostId", "CourseId", "CreationDate", "IsVisible", "Name", "PostType", "TextContent" },
                values: new object[,]
                {
                    { 30000, 10000, new DateTime(2023, 12, 6, 21, 0, 25, 192, DateTimeKind.Local).AddTicks(3634), true, "Introduction", 0, "This is a placeholder" },
                    { 30001, 10002, new DateTime(2023, 12, 6, 21, 0, 25, 192, DateTimeKind.Local).AddTicks(3648), true, "Knowledge", 0, "This is once more a placeholder https://www.youtube.com/watch?v=dQw4w9WgXcQ" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "CourseId", "CreationDate", "FileName", "IsVisible", "Name", "PostType" },
                values: new object[] { 30003, 10001, new DateTime(2023, 12, 6, 21, 0, 25, 192, DateTimeKind.Local).AddTicks(3669), "tvarkarastis.jpg", true, "File", 1 });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "CourseId", "UserId", "UserRating" },
                values: new object[,]
                {
                    { 10000, 20000, 3m },
                    { 10001, 20000, 5m },
                    { 10000, 20001, 4m },
                    { 10000, 20002, 3m }
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

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_CourseId",
                table: "Ratings",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "CourseUsers");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
