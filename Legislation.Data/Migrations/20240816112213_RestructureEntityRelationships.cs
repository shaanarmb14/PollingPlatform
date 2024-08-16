using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Legislation.Data.Migrations
{
    /// <inheritdoc />
    public partial class RestructureEntityRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Laws_Referendums_ReferendumID",
                table: "Laws");

            migrationBuilder.DropIndex(
                name: "IX_LawEntity_ReferendumId",
                table: "Laws");

            migrationBuilder.DropColumn(
                name: "NoVotes",
                table: "Laws");

            migrationBuilder.DropColumn(
                name: "ReferendumID",
                table: "Laws");

            migrationBuilder.DropColumn(
                name: "YesVotes",
                table: "Laws");

            migrationBuilder.AddColumn<int>(
                name: "LawID",
                table: "Referendums",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ReferendumEntity_LawID",
                table: "Referendums",
                column: "LawID");

            migrationBuilder.AddForeignKey(
                name: "FK_Referendums_Laws_LawID",
                table: "Referendums",
                column: "LawID",
                principalTable: "Laws",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Referendums_Laws_LawID",
                table: "Referendums");

            migrationBuilder.DropIndex(
                name: "IX_ReferendumEntity_LawID",
                table: "Referendums");

            migrationBuilder.DropColumn(
                name: "LawID",
                table: "Referendums");

            migrationBuilder.AddColumn<int>(
                name: "NoVotes",
                table: "Laws",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReferendumID",
                table: "Laws",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YesVotes",
                table: "Laws",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LawEntity_ReferendumId",
                table: "Laws",
                column: "ReferendumID");

            migrationBuilder.AddForeignKey(
                name: "FK_Laws_Referendums_ReferendumID",
                table: "Laws",
                column: "ReferendumID",
                principalTable: "Referendums",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
