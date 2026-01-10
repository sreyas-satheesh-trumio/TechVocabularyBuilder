using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechVocabulary.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTopicsLearnedTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "TopicsLearned");
        }
    }
}
