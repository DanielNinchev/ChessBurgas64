using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessBurgas64.Data.Migrations
{
    public partial class AddPuzzleObjectiveAndMakeNameToNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Puzzles",
                newName: "Objective");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Puzzles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Puzzles");

            migrationBuilder.RenameColumn(
                name: "Objective",
                table: "Puzzles",
                newName: "Name");
        }
    }
}
