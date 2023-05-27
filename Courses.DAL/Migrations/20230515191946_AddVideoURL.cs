using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Courses.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddVideoURL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VideoURL",
                table: "Course",
                type: "nvarchar(2083)",
                maxLength: 2083,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "CompletedPart",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SubscribedCourse",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscribedCourse", x => new { x.UserId, x.CourseId });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscribedCourse");

            migrationBuilder.DropColumn(
                name: "VideoURL",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "CompletedPart");
        }
    }
}
