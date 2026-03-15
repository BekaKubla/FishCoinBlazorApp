using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishCoinBlazorApp.Migrations
{
    /// <inheritdoc />
    public partial class AddPointsPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PointsPrice",
                table: "Products",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PointsPrice",
                table: "Products");
        }
    }
}
