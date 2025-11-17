using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreSaleForm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PreSaleForms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerFullName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CustomerPhone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DoorModel = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    DoorSurfaceType = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    DoorLeafWidth = table.Column<decimal>(type: "numeric", nullable: false),
                    DoorLeafHeight = table.Column<decimal>(type: "numeric", nullable: false),
                    DoorFrameWidth = table.Column<decimal>(type: "numeric", nullable: false),
                    DoorQuantity = table.Column<int>(type: "integer", nullable: false),
                    IsWithGlass = table.Column<bool>(type: "boolean", nullable: false),
                    Color = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Note = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    TotalAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    RemainingAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    TermsAccepted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PdfFilePath = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreSaleForms", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PreSaleForms");
        }
    }
}
