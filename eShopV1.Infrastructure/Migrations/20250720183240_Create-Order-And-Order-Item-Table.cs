using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopV1.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateOrderAndOrderItemTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "delivery_methods",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    short_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    delivery_time = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    price = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_delivery_methods", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    subtotal = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    discount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    currency = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    applied_coupon_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    applied_coupon_amount_off = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    applied_coupon_percent_off = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    applied_coupon_promotion_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    delivery_method_id = table.Column<Guid>(type: "uuid", nullable: false),
                    shipping_address_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    shipping_address_line1 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    shipping_address_line2 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    shipping_address_city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    shipping_address_state = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    shipping_address_postal_code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    shipping_address_country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    order_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.id);
                    table.ForeignKey(
                        name: "fk_orders_delivery_methods_delivery_method_id",
                        column: x => x.delivery_method_id,
                        principalTable: "delivery_methods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "order_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_item_ordered_product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_item_ordered_product_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    product_item_ordered_picture_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_items_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_order_items_order_id",
                table: "order_items",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_delivery_method_id",
                table: "orders",
                column: "delivery_method_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_items");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "delivery_methods");
        }
    }
}
