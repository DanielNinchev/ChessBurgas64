using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessBurgas64.Data.Migrations
{
    public partial class AddNotesToLesson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Lessons");
        }
    }
}
