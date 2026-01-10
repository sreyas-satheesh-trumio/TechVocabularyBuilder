using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechVocabulary.API.Migrations
{
    /// <inheritdoc />
    public partial class AddGameProgressTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameProgresses",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    AttemptedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameProgresses", x => x.GameId);
                    table.ForeignKey(
                        name: "FK_GameProgresses_EndUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "EndUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameProgresses_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameProgresses_TopicId",
                table: "GameProgresses",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_GameProgresses_UserId_TopicId",
                table: "GameProgresses",
                columns: new[] { "UserId", "TopicId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameProgresses");
        }
    }
}
