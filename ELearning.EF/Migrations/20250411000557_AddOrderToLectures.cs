using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELearning.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderToLectures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Lectures_SectionId",
                table: "Lectures");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Lectures",
                newName: "Title");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Lectures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_SectionId_Order",
                table: "Lectures",
                columns: new[] { "SectionId", "Order" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Lectures_SectionId_Order",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Lectures");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Lectures",
                newName: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_SectionId",
                table: "Lectures",
                column: "SectionId");
        }
    }
}
