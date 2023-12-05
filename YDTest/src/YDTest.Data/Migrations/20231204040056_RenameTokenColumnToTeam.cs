using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YDTest.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameTokenColumnToTeam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Token",
                table: "Teams",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Teams",
                newName: "Token");
        }
    }
}
