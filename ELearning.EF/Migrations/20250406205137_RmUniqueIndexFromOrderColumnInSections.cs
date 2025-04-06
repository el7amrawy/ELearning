using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELearning.EF.Migrations
{
    /// <inheritdoc />
    public partial class RmUniqueIndexFromOrderColumnInSections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sections_Order",
                table: "Sections");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Sections_Order",
                table: "Sections",
                column: "Order",
                unique: true);
        }
    }
}
