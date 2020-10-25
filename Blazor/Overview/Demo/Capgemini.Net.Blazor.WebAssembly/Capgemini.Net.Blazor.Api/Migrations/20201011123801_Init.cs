using Microsoft.EntityFrameworkCore.Migrations;

namespace Capgemini.Net.Blazor.Api.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "RateRanges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinRate = table.Column<int>(nullable: false),
                    MaxRate = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateRanges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    AverageRate = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    CategoryFK = table.Column<string>(nullable: false),
                    RateRangeFK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryFK",
                        column: x => x.CategoryFK,
                        principalTable: "Categories",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_RateRanges_RateRangeFK",
                        column: x => x.RateRangeFK,
                        principalTable: "RateRanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductRates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductFK = table.Column<int>(nullable: false),
                    Rate = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductRates_Products_ProductFK",
                        column: x => x.ProductFK,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductRates_ProductFK",
                table: "ProductRates",
                column: "ProductFK");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryFK",
                table: "Products",
                column: "CategoryFK");

            migrationBuilder.CreateIndex(
                name: "IX_Products_RateRangeFK",
                table: "Products",
                column: "RateRangeFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductRates");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "RateRanges");
        }
    }
}
