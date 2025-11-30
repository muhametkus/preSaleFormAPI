using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreSaleForm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDiscountFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount",
                table: "PreSaleForms",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountedAmount",
                table: "PreSaleForms",
                type: "numeric",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "PreSaleForms");

            migrationBuilder.DropColumn(
                name: "DiscountedAmount",
                table: "PreSaleForms");
        }
    }
}
