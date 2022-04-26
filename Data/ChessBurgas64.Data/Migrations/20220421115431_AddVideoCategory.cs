using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessBurgas64.Data.Migrations
{
    public partial class AddVideoCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Videos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "VideoCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Videos_CategoryId",
                table: "Videos",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoCategories_IsDeleted",
                table: "VideoCategories",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_VideoCategories_CategoryId",
                table: "Videos",
                column: "CategoryId",
                principalTable: "VideoCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_VideoCategories_CategoryId",
                table: "Videos");

            migrationBuilder.DropTable(
                name: "VideoCategories");

            migrationBuilder.DropIndex(
                name: "IX_Videos_CategoryId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Videos");
        }
    }
}
