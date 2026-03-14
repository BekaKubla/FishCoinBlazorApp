using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishCoinBlazorApp.Migrations
{
    /// <inheritdoc />
    public partial class CommentHunterCaategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName" },
                values: new object[] { 2, "სანადირო" });
        }
    }
}
