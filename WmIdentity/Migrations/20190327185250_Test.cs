using Microsoft.EntityFrameworkCore.Migrations;

namespace WmIdentity.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSubcategories",
                table: "ProductSubcategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductSubcategories_SubCategoryId",
                table: "ProductSubcategories");

            migrationBuilder.AlterColumn<string>(
                name: "PhotoPath",
                table: "Products",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSubcategories",
                table: "ProductSubcategories",
                columns: new[] { "SubCategoryId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubcategories_ProductId",
                table: "ProductSubcategories",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSubcategories",
                table: "ProductSubcategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductSubcategories_ProductId",
                table: "ProductSubcategories");

            migrationBuilder.AlterColumn<string>(
                name: "PhotoPath",
                table: "Products",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSubcategories",
                table: "ProductSubcategories",
                columns: new[] { "ProductId", "SubCategoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubcategories_SubCategoryId",
                table: "ProductSubcategories",
                column: "SubCategoryId");
        }
    }
}
