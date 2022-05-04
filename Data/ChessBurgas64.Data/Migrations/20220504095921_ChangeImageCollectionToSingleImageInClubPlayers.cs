using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChessBurgas64.Data.Migrations
{
    public partial class ChangeImageCollectionToSingleImageInClubPlayers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Images_ClubPlayerId",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "ClubPlayers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Images_ClubPlayerId",
                table: "Images",
                column: "ClubPlayerId",
                unique: true,
                filter: "[ClubPlayerId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Images_ClubPlayerId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "ClubPlayers");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ClubPlayerId",
                table: "Images",
                column: "ClubPlayerId");
        }
    }
}
