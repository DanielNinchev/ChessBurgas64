using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChessBurgas64.Data.Migrations
{
    public partial class RemoveUserImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Images_ImageId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ImageId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ImageId",
                table: "AspNetUsers",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Images_ImageId",
                table: "AspNetUsers",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }
    }
}
