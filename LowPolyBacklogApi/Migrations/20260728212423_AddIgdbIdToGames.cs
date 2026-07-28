using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LowPolyBacklogApi.Migrations
{
    /// <inheritdoc />
    public partial class AddIgdbIdToGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IgdbId",
                table: "Games",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IgdbId",
                table: "Games");
        }
    }
}
