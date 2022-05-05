using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChessBurgas64.Data.Migrations
{
    public partial class ChangeClubPlayerToNotableMemberAndRemoveTrainerImagesAndDateOfJoningCurrentGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_ClubPlayers_ClubPlayerId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Trainers_TrainerId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "ClubPlayers");

            migrationBuilder.DropIndex(
                name: "IX_Images_ClubPlayerId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_TrainerId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "DateOfLastAttendance",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "DateOfJoiningCurrentGroup",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "LearnedOpenings",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "TrainerId",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "ClubPlayerId",
                table: "Images",
                newName: "NotableMemberId");

            migrationBuilder.CreateTable(
                name: "NotableMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FideTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPartOfGovernance = table.Column<bool>(type: "bit", nullable: false),
                    ListIndex = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotableMembers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_NotableMemberId",
                table: "Images",
                column: "NotableMemberId",
                unique: true,
                filter: "[NotableMemberId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NotableMembers_IsDeleted",
                table: "NotableMembers",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_NotableMembers_NotableMemberId",
                table: "Images",
                column: "NotableMemberId",
                principalTable: "NotableMembers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_NotableMembers_NotableMemberId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "NotableMembers");

            migrationBuilder.DropIndex(
                name: "IX_Images_NotableMemberId",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "NotableMemberId",
                table: "Images",
                newName: "ClubPlayerId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfLastAttendance",
                table: "Trainers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "Trainers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfJoiningCurrentGroup",
                table: "Members",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LearnedOpenings",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrainerId",
                table: "Images",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClubPlayers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FideTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubPlayers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_ClubPlayerId",
                table: "Images",
                column: "ClubPlayerId",
                unique: true,
                filter: "[ClubPlayerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Images_TrainerId",
                table: "Images",
                column: "TrainerId",
                unique: true,
                filter: "[TrainerId] IS NOT NULL");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Trainers_TrainerId",
                table: "Images",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id");
        }
    }
}
