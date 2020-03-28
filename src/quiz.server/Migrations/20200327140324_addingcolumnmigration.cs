using Microsoft.EntityFrameworkCore.Migrations;

namespace quiz.server.Migrations
{
    public partial class addingcolumnmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GamePlayed",
                table: "Leaderboards",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GamePlayed",
                table: "Leaderboards");
        }
    }
}
