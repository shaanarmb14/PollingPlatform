using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Results.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOutcomeColumm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Outcome",
                table: "Results",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Outcome",
                table: "Results");
        }
    }
}
