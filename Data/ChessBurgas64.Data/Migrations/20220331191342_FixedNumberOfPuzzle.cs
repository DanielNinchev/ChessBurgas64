using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessBurgas64.Data.Migrations
{
    public partial class FixedNumberOfPuzzle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PuzzleNumber",
                table: "Puzzles",
                newName: "NumberOfPuzzle");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfPuzzle",
                table: "Puzzles",
                newName: "PuzzleNumber");
        }
    }
}
