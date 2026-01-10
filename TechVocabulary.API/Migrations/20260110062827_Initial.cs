using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechVocabulary.API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EndUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "User"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndUsers", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    TopicId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopicName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Definition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RealWorldUsage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodeSnippet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.TopicId);
                    table.ForeignKey(
                        name: "FK_Topics_EndUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "EndUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "TopicsLearned",
                columns: table => new
                {
                    TopicLearnedId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    LearnedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicsLearned", x => x.TopicLearnedId);
                    table.ForeignKey(
                        name: "FK_TopicsLearned_EndUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "EndUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TopicsLearned_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EndUsers_Email",
                table: "EndUsers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EndUsers_Username",
                table: "EndUsers",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameProgresses_TopicId",
                table: "GameProgresses",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_GameProgresses_UserId_TopicId",
                table: "GameProgresses",
                columns: new[] { "UserId", "TopicId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Topics_CreatedBy",
                table: "Topics",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TopicsLearned_TopicId",
                table: "TopicsLearned",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicsLearned_UserId_TopicId",
                table: "TopicsLearned",
                columns: new[] { "UserId", "TopicId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameProgresses");

            migrationBuilder.DropTable(
                name: "TopicsLearned");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropTable(
                name: "EndUsers");
        }
    }
}
