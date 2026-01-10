using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechVocabulary.API.Migrations
{
    /// <inheritdoc />
    public partial class UserTableCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "EndUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "User",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldDefaultValue: "User");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "EndUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_EndUsers_Username",
                table: "EndUsers",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EndUsers_Username",
                table: "EndUsers");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "EndUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "EndUsers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "User",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "User");
        }
    }
}
