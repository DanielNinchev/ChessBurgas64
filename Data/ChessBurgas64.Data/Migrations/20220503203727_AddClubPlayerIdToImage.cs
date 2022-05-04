using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChessBurgas64.Data.Migrations
{
    public partial class AddClubPlayerIdToImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClubPlayerId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClubPlayers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FideTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubPlayers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_ClubPlayerId",
                table: "Images",
                column: "ClubPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubPlayers_IsDeleted",
                table: "ClubPlayers",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_ClubPlayers_ClubPlayerId",
                table: "Images",
                column: "ClubPlayerId",
                principalTable: "ClubPlayers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_ClubPlayers_ClubPlayerId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "ClubPlayers");

            migrationBuilder.DropIndex(
                name: "IX_Images_ClubPlayerId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ClubPlayerId",
                table: "Images");
        }
    }
}
