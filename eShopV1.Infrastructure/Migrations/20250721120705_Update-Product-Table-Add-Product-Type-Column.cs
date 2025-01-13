using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopV1.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductTableAddProductTypeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "products",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                table: "products");
        }
    }
}
