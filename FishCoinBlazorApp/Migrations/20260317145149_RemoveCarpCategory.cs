using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FishCoinBlazorApp.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCarpCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                keyValue: 6);

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
                columns: new[] { "ProductCategoryName", "SubCategoryId" },
                values: new object[] { "ნემსკავი/სადავე", 3 });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "ProductCategoryName", "SubCategoryId" },
                values: new object[] { "ძუა/წნული", 3 });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "ProductCategoryName", "SubCategoryId" },
                values: new object[] { "დასაკვები/დანამატები", 3 });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "ProductCategoryName", "SubCategoryId" },
                values: new object[] { "საკვებურა", 3 });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "ProductCategoryName", "SubCategoryId" },
                values: new object[] { "აღჭურვილობა (სკამი/ბადე)", 3 });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "ProductCategoryName", "SubCategoryId" },
                values: new object[] { "აქსესუარები", 3 });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "ProductCategoryName", "SubCategoryId" },
                values: new object[] { "ჯოხი", 4 });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "ProductCategoryName", "SubCategoryId" },
                values: new object[] { "კოჭა", 4 });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "ProductCategoryName", "SubCategoryId" },
                values: new object[] { "ტივტივა", 4 });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "ProductCategoryName", "SubCategoryId" },
                values: new object[] { "ნემსკავი/ძუა/სიმძიმე", 4 });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "ProductCategoryName", "SubCategoryId" },
                values: new object[] { "აქსესუარები", 4 });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "SubCategoryName",
                value: "ფიდერი");

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "SubCategoryName",
                value: "ტივტივა");

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 4,
                column: "SubCategoryName",
                value: "აქსესუარი");

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 5,
                column: "SubCategoryName",
                value: "საჩუქარი");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                columns: new[] { "ProductCategoryName", "SubCategoryId" },
                values: new object[] { "ნემსკავი/რიგები", 2 });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "ProductCategoryName", "SubCategoryId" },
                values: new object[] { "ძუა/ლიდკორი", 2 });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "ProductCategoryName", "SubCategoryId" },
                values: new object[] { "სვიველი/PVA/სიმძიმე", 2 });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "ProductCategoryName", "SubCategoryId" },
                values: new object[] { "სატყუარა/ბოილი/პელეტსი", 2 });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "ProductCategoryName", "SubCategoryId" },
                values: new object[] { "პოდი/სიგნალიზატორი", 2 });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "ProductCategoryName", "SubCategoryId" },
                values: new object[] { "ინვენტარი (სკამი/ბადე)", 2 });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "ProductCategoryName", "SubCategoryId" },
                values: new object[] { "აქსესუარები", 2 });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "ProductCategoryName", "SubCategoryId" },
                values: new object[] { "ჯოხი", 3 });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "ProductCategoryName", "SubCategoryId" },
                values: new object[] { "კოჭა", 3 });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "ProductCategoryName", "SubCategoryId" },
                values: new object[] { "ნემსკავი/სადავე", 3 });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "ProductCategoryName", "SubCategoryId" },
                values: new object[] { "ძუა/წნული", 3 });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "ProductCategoryName", "SubCategoryId" },
                values: new object[,]
                {
                    { 24, "დასაკვები/დანამატები", 3 },
                    { 25, "საკვებურა", 3 },
                    { 26, "აღჭურვილობა (სკამი/ბადე)", 3 },
                    { 27, "აქსესუარები", 3 },
                    { 28, "ჯოხი", 4 },
                    { 29, "კოჭა", 4 },
                    { 30, "ტივტივა", 4 },
                    { 31, "ნემსკავი/ძუა/სიმძიმე", 4 },
                    { 32, "აქსესუარები", 4 }
                });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "SubCategoryName",
                value: "კარპი");

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "SubCategoryName",
                value: "ფიდერი");

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 4,
                column: "SubCategoryName",
                value: "ტივტივა");

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 5,
                column: "SubCategoryName",
                value: "აქსესუარი");

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryId", "SubCategoryName" },
                values: new object[] { 6, 1, "საჩუქარი" });
        }
    }
}
