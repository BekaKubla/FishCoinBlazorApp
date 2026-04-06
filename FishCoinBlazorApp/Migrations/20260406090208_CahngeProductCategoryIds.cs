using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishCoinBlazorApp.Migrations
{
    /// <inheritdoc />
    public partial class CahngeProductCategoryIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 11,
                column: "SubCategoryId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 12,
                column: "SubCategoryId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 13,
                column: "SubCategoryId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 14,
                column: "SubCategoryId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 15,
                column: "SubCategoryId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 16,
                column: "SubCategoryId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 17,
                column: "SubCategoryId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 18,
                column: "SubCategoryId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 19,
                column: "SubCategoryId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 20,
                column: "SubCategoryId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 21,
                column: "SubCategoryId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 22,
                column: "SubCategoryId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 23,
                column: "SubCategoryId",
                value: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 11,
                column: "SubCategoryId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 12,
                column: "SubCategoryId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 13,
                column: "SubCategoryId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 14,
                column: "SubCategoryId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 15,
                column: "SubCategoryId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 16,
                column: "SubCategoryId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 17,
                column: "SubCategoryId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 18,
                column: "SubCategoryId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 19,
                column: "SubCategoryId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 20,
                column: "SubCategoryId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 21,
                column: "SubCategoryId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 22,
                column: "SubCategoryId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 23,
                column: "SubCategoryId",
                value: 4);
        }
    }
}
