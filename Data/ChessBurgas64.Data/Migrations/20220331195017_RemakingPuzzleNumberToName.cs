using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessBurgas64.Data.Migrations
{
    public partial class RemakingPuzzleNumberToName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FollowingNum",
                table: "Puzzles");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Puzzles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Puzzles");

            migrationBuilder.AddColumn<int>(
                name: "FollowingNum",
                table: "Puzzles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
