using Microsoft.EntityFrameworkCore.Migrations;

namespace quiz.server.Migrations
{
    public partial class addingtablemigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayedMatches",
                columns: table => new
                {
                    Username = table.Column<string>(nullable: false),
                    HasPlayed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayedMatches", x => x.Username);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayedMatches");
        }
    }
}
