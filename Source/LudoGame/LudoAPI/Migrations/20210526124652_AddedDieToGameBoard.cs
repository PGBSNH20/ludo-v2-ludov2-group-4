using Microsoft.EntityFrameworkCore.Migrations;

namespace LudoAPI.Migrations
{
    public partial class AddedDieToGameBoard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Die",
                table: "GameBoards",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Die",
                table: "GameBoards");
        }
    }
}
