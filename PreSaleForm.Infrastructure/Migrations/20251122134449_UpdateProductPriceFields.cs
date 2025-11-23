using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreSaleForm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductPriceFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Products",
                newName: "PriceWithoutAssembly");

            migrationBuilder.AddColumn<decimal>(
                name: "PriceWithAssembly",
                table: "Products",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceWithAssembly",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "PriceWithoutAssembly",
                table: "Products",
                newName: "Price");
        }
    }
}
