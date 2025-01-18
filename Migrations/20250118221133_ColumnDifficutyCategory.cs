using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tasks_ef.Migrations
{
    /// <inheritdoc />
    public partial class ColumnDifficutyCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Difficulty",
                table: "Category",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Difficulty",
                table: "Category");
        }
    }
}
