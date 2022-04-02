using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessBurgas64.Data.Migrations
{
    public partial class ChangedPuzzleNameToNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Puzzles");

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

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Puzzles",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
