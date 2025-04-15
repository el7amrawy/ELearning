using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELearning.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddDurationToSectionsAndCourses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "Courses");

            migrationBuilder.AddColumn<double>(
                name: "Duration",
                table: "Sections",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Duration",
                table: "Courses",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Courses");

            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
