using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreSaleForm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDismantlingFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DismantlingUnitPrice",
                table: "PreSaleForms",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OldDoorCount",
                table: "PreSaleForms",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalDismantlingPrice",
                table: "PreSaleForms",
                type: "numeric",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DismantlingUnitPrice",
                table: "PreSaleForms");

            migrationBuilder.DropColumn(
                name: "OldDoorCount",
                table: "PreSaleForms");

            migrationBuilder.DropColumn(
                name: "TotalDismantlingPrice",
                table: "PreSaleForms");
        }
    }
}
