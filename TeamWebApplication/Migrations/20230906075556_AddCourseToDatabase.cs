using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamWebApplication.Migrations
{
    public partial class AddCourseToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsHidden",
                table: "Courses",
                newName: "IsVisible");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Courses",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsVisible",
                table: "Courses",
                newName: "IsHidden");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Courses",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}
