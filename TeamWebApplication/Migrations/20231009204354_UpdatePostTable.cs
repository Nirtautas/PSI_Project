using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamWebApplication.Migrations
{
    public partial class UpdatePostTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Courses_CourseId",
                table: "Post");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Post",
                table: "Post");

            migrationBuilder.RenameTable(
                name: "Post",
                newName: "Posts");

            migrationBuilder.RenameColumn(
                name: "UsersSurnameThatCommented",
                table: "Comments",
                newName: "CommentatorSurname");

            migrationBuilder.RenameColumn(
                name: "UsersNameThatCommented",
                table: "Comments",
                newName: "CommentatorName");

            migrationBuilder.RenameColumn(
                name: "CommentCreationTime",
                table: "Comments",
                newName: "CreationTime");

            migrationBuilder.RenameIndex(
                name: "IX_Post_CourseId",
                table: "Posts",
                newName: "IX_Posts_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Courses_CourseId",
                table: "Posts",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Courses_CourseId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "Post");

            migrationBuilder.RenameColumn(
                name: "CreationTime",
                table: "Comments",
                newName: "CommentCreationTime");

            migrationBuilder.RenameColumn(
                name: "CommentatorSurname",
                table: "Comments",
                newName: "UsersSurnameThatCommented");

            migrationBuilder.RenameColumn(
                name: "CommentatorName",
                table: "Comments",
                newName: "UsersNameThatCommented");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_CourseId",
                table: "Post",
                newName: "IX_Post_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Post",
                table: "Post",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Courses_CourseId",
                table: "Post",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
