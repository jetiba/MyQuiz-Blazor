using Microsoft.EntityFrameworkCore.Migrations;

namespace quiz.server.Migrations
{
    public partial class addinghasplayedlastgamecolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayedMatches");

            migrationBuilder.AddColumn<bool>(
                name: "HasPlayedLastGame",
                table: "Leaderboards",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasPlayedLastGame",
                table: "Leaderboards");

            migrationBuilder.CreateTable(
                name: "PlayedMatches",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HasPlayed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayedMatches", x => x.Username);
                });
        }
    }
}
