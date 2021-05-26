using Microsoft.EntityFrameworkCore.Migrations;

namespace LudoAPI.Migrations
{
    public partial class ChangedCurrentPlayerIdToCurrentPlayerIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrentPlayerId",
                table: "GameBoards",
                newName: "CurrentPlayerIndex");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrentPlayerIndex",
                table: "GameBoards",
                newName: "CurrentPlayerId");
        }
    }
}
