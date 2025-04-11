using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELearning.EF.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDescriptionsToNotesInLectures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Lectures",
                newName: "Notes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Lectures",
                newName: "Description");
        }
    }
}
