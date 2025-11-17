using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreSaleForm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreatePreSaleFormAndProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "PreSaleForms");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "PreSaleForms");

            migrationBuilder.DropColumn(
                name: "DoorFrameWidth",
                table: "PreSaleForms");

            migrationBuilder.DropColumn(
                name: "DoorLeafHeight",
                table: "PreSaleForms");

            migrationBuilder.DropColumn(
                name: "DoorLeafWidth",
                table: "PreSaleForms");

            migrationBuilder.DropColumn(
                name: "DoorModel",
                table: "PreSaleForms");

            migrationBuilder.DropColumn(
                name: "DoorQuantity",
                table: "PreSaleForms");

            migrationBuilder.DropColumn(
                name: "DoorSurfaceType",
                table: "PreSaleForms");

            migrationBuilder.DropColumn(
                name: "IsWithGlass",
                table: "PreSaleForms");

            migrationBuilder.DropColumn(
                name: "TermsAccepted",
                table: "PreSaleForms");

            migrationBuilder.CreateTable(
                name: "PreSaleFormProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PreSaleFormId = table.Column<Guid>(type: "uuid", nullable: false),
                    DoorModel = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    DoorSurfaceType = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    DoorLeafWidth = table.Column<decimal>(type: "numeric", nullable: false),
                    DoorLeafHeight = table.Column<decimal>(type: "numeric", nullable: false),
                    DoorFrameWidth = table.Column<decimal>(type: "numeric", nullable: false),
                    DoorQuantity = table.Column<int>(type: "integer", nullable: false),
                    IsWithGlass = table.Column<bool>(type: "boolean", nullable: false),
                    Color = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreSaleFormProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreSaleFormProducts_PreSaleForms_PreSaleFormId",
                        column: x => x.PreSaleFormId,
                        principalTable: "PreSaleForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PreSaleFormProducts_PreSaleFormId",
                table: "PreSaleFormProducts",
                column: "PreSaleFormId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PreSaleFormProducts");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "PreSaleForms",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "PreSaleForms",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "DoorFrameWidth",
                table: "PreSaleForms",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DoorLeafHeight",
                table: "PreSaleForms",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DoorLeafWidth",
                table: "PreSaleForms",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "DoorModel",
                table: "PreSaleForms",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DoorQuantity",
                table: "PreSaleForms",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DoorSurfaceType",
                table: "PreSaleForms",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsWithGlass",
                table: "PreSaleForms",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TermsAccepted",
                table: "PreSaleForms",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
