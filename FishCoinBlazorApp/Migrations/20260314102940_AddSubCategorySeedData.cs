using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FishCoinBlazorApp.Migrations
{
    /// <inheritdoc />
    public partial class AddSubCategorySeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "ProductCategoryName",
                value: "ჯოხი");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "ProductCategoryName",
                value: "კოჭა");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "ProductCategoryName",
                value: "ნემსკავი");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 4,
                column: "ProductCategoryName",
                value: "ძუა/წნული/ფლურო");

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "ProductCategoryName", "SubCategoryId" },
                values: new object[,]
                {
                    { 5, "ვობლერი", 1 },
                    { 6, "ტრიალა", 1 },
                    { 7, "ყანყალა", 1 },
                    { 8, "ჯიგთავი", 1 },
                    { 9, "სილიკონი", 1 },
                    { 10, "აქსესუარები", 1 },
                    { 11, "ჯოხი", 2 },
                    { 12, "კოჭა", 2 },
                    { 13, "ნემსკავი/რიგები", 2 },
                    { 14, "ძუა/ლიდკორი", 2 },
                    { 15, "სვიველი/PVA/სიმძიმე", 2 },
                    { 16, "სატყუარა/ბოილი/პელეტსი", 2 },
                    { 17, "პოდი/სიგნალიზატორი", 2 },
                    { 18, "ინვენტარი (სკამი/ბადე)", 2 },
                    { 19, "აქსესუარები", 2 },
                    { 20, "ჯოხი", 3 },
                    { 21, "კოჭა", 3 },
                    { 22, "ნემსკავი/სადავე", 3 },
                    { 23, "ძუა/წნული", 3 },
                    { 24, "დასაკვები/დანამატები", 3 },
                    { 25, "საკვებურა", 3 },
                    { 26, "აღჭურვილობა (სკამი/ბადე)", 3 },
                    { 27, "აქსესუარები", 3 }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryId", "SubCategoryName" },
                values: new object[,]
                {
                    { 4, 1, "ტივტივა" },
                    { 5, 1, "აქსესუარი" },
                    { 6, 1, "საჩუქარი" }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "ProductCategoryName", "SubCategoryId" },
                values: new object[,]
                {
                    { 28, "ჯოხი", 4 },
                    { 29, "კოჭა", 4 },
                    { 30, "ტივტივა", 4 },
                    { 31, "ნემსკავი/ძუა/სიმძიმე", 4 },
                    { 32, "აქსესუარები", 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "ProductCategoryName",
                value: "ჯოხები");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "ProductCategoryName",
                value: "კოჭები");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "ProductCategoryName",
                value: "სატყუარები");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 4,
                column: "ProductCategoryName",
                value: "აქსესუარები");
        }
    }
}
