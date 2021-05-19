using Microsoft.EntityFrameworkCore.Migrations;

namespace LudoAPI.Migrations
{
    public partial class DeletedNotMappedOnGameBoardPlayers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Players_GameBoardId",
                table: "Players",
                column: "GameBoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_GameBoards_GameBoardId",
                table: "Players",
                column: "GameBoardId",
                principalTable: "GameBoards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_GameBoards_GameBoardId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_GameBoardId",
                table: "Players");
        }
    }
}
