using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechVocabulary.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTopicsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_Topics_CreatedBy",
                table: "Topics",
                column: "CreatedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Topics");
        }
    }
}
