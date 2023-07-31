using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.Host.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "catalog_brand_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "catalog_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "catalog_type_hilo",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "CatalogBrand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Brand = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogBrand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Catalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    PictureFileName = table.Column<string>(type: "text", nullable: true),
                    CatalogTypeId = table.Column<int>(type: "integer", nullable: false),
                    CatalogBrandId = table.Column<int>(type: "integer", nullable: false),
                    AvailableStock = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Catalog_CatalogBrand_CatalogBrandId",
                        column: x => x.CatalogBrandId,
                        principalTable: "CatalogBrand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Catalog_CatalogType_CatalogTypeId",
                        column: x => x.CatalogTypeId,
                        principalTable: "CatalogType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CatalogBrand",
                columns: new[] { "Id", "Brand" },
                values: new object[,]
                {
                    { 1, "Azure" },
                    { 2, ".NET" },
                    { 3, "Visual Studio" },
                    { 4, "SQL Server" },
                    { 5, "Other" }
                });

            migrationBuilder.InsertData(
                table: "CatalogType",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Mug" },
                    { 2, "T-Shirt" },
                    { 3, "Sheet" },
                    { 4, "USB Memory Stick" }
                });

            migrationBuilder.InsertData(
                table: "Catalog",
                columns: new[] { "Id", "AvailableStock", "CatalogBrandId", "CatalogTypeId", "Description", "Name", "PictureFileName", "Price" },
                values: new object[,]
                {
                    { 1, 100, 2, 2, ".NET Bot Black Hoodie", ".NET Bot Black Hoodie", "1.png", 19.5m },
                    { 2, 100, 2, 1, ".NET Black & White Mug", ".NET Black & White Mug", "2.png", 8.50m },
                    { 3, 100, 5, 2, "Prism White T-Shirt", "Prism White T-Shirt", "3.png", 12m },
                    { 4, 100, 2, 2, ".NET Foundation T-shirt", ".NET Foundation T-shirt", "4.png", 12m },
                    { 5, 100, 5, 3, "Roslyn Red Sheet", "Roslyn Red Sheet", "5.png", 8.5m },
                    { 6, 100, 2, 2, ".NET Blue Hoodie", ".NET Blue Hoodie", "6.png", 12m },
                    { 7, 100, 5, 2, "Roslyn Red T-Shirt", "Roslyn Red T-Shirt", "7.png", 12m },
                    { 8, 100, 5, 2, "Kudu Purple Hoodie", "Kudu Purple Hoodie", "8.png", 8.5m },
                    { 9, 100, 5, 1, "Cup<T> White Mug", "Cup<T> White Mug", "9.png", 12m },
                    { 10, 100, 2, 3, ".NET Foundation Sheet", ".NET Foundation Sheet", "10.png", 12m },
                    { 11, 100, 2, 3, "Cup<T> Sheet", "Cup<T> Sheet", "11.png", 8.5m },
                    { 12, 100, 5, 2, "Prism White TShirt", "Prism White TShirt", "12.png", 12m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Catalog_CatalogBrandId",
                table: "Catalog",
                column: "CatalogBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Catalog_CatalogTypeId",
                table: "Catalog",
                column: "CatalogTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Catalog");

            migrationBuilder.DropTable(
                name: "CatalogBrand");

            migrationBuilder.DropTable(
                name: "CatalogType");

            migrationBuilder.DropSequence(
                name: "catalog_brand_hilo");

            migrationBuilder.DropSequence(
                name: "catalog_hilo");

            migrationBuilder.DropSequence(
                name: "catalog_type_hilo");
        }
    }
}
