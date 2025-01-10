using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopV1.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateShoppingCartAndCartItemTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "shopping_carts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    buyer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    coupon_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    coupon_amount_off = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    coupon_percent_off = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    coupon_promotion_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    client_secret = table.Column<string>(type: "text", nullable: true),
                    payment_intent_id = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shopping_carts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cart_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    shopping_cart_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    picture_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    currency = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cart_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_cart_items_shopping_carts_shopping_cart_id",
                        column: x => x.shopping_cart_id,
                        principalTable: "shopping_carts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_cart_items_shopping_cart_id",
                table: "cart_items",
                column: "shopping_cart_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cart_items");

            migrationBuilder.DropTable(
                name: "shopping_carts");
        }
    }
}
