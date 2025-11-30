using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreSaleForm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceAndAccessoryFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AksesuarDahilMi",
                table: "PreSaleForms",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AksesuarUcreti",
                table: "PreSaleForms",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FabrikaTeslimMi",
                table: "PreSaleForms",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MontajDahilMi",
                table: "PreSaleForms",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NakliyeDahilMi",
                table: "PreSaleForms",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "NakliyeUcreti",
                table: "PreSaleForms",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecilenAksesuar",
                table: "PreSaleForms",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AksesuarDahilMi",
                table: "PreSaleForms");

            migrationBuilder.DropColumn(
                name: "AksesuarUcreti",
                table: "PreSaleForms");

            migrationBuilder.DropColumn(
                name: "FabrikaTeslimMi",
                table: "PreSaleForms");

            migrationBuilder.DropColumn(
                name: "MontajDahilMi",
                table: "PreSaleForms");

            migrationBuilder.DropColumn(
                name: "NakliyeDahilMi",
                table: "PreSaleForms");

            migrationBuilder.DropColumn(
                name: "NakliyeUcreti",
                table: "PreSaleForms");

            migrationBuilder.DropColumn(
                name: "SecilenAksesuar",
                table: "PreSaleForms");
        }
    }
}
