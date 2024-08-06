using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Legislation.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddYesNoVotesToLawEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Votes",
                table: "Laws",
                newName: "YesVotes");

            migrationBuilder.AddColumn<int>(
                name: "NoVotes",
                table: "Laws",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoVotes",
                table: "Laws");

            migrationBuilder.RenameColumn(
                name: "YesVotes",
                table: "Laws",
                newName: "Votes");
        }
    }
}
