using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Courses.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CourseSubscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PraticalPartId",
                table: "CompletedPart",
                newName: "PracticalPartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PracticalPartId",
                table: "CompletedPart",
                newName: "PraticalPartId");
        }
    }
}
