using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Legislation.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameReferendumEndendToOpen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ended",
                table: "Referendums");

            migrationBuilder.AddColumn<bool>(
                name: "Open",
                table: "Referendums",
                type: "boolean",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Open",
                table: "Referendums");

            migrationBuilder.AddColumn<bool>(
                name: "Ended",
                table: "Referendums",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
