using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace quiz.server.Data.QuizMigrations
{
    public partial class InitialQuizMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Leaderboards",
                columns: table => new
                {
                    Username = table.Column<string>(nullable: false),
                    Points = table.Column<int>(nullable: false),
                    GamePlayed = table.Column<int>(nullable: false),
                    HasPlayedLastGame = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leaderboards", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionID = table.Column<Guid>(nullable: false),
                    QuestionText = table.Column<string>(nullable: true),
                    Answer1 = table.Column<string>(nullable: true),
                    Answer2 = table.Column<string>(nullable: true),
                    Answer3 = table.Column<string>(nullable: true),
                    Solution = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Leaderboards");

            migrationBuilder.DropTable(
                name: "Questions");
        }
    }
}
