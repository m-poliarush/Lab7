using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DomainData.Migrations
{
    /// <inheritdoc />
    public partial class newMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseMenuItem",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    ItemType = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseMenuItem", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DailyMenus",
                columns: table => new
                {
                    DayID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DayOfWeek = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyMenus", x => x.DayID);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TotalCost = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                });

            migrationBuilder.CreateTable(
                name: "ComplexDishDish",
                columns: table => new
                {
                    DishListID = table.Column<int>(type: "integer", nullable: false),
                    complexDishesID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplexDishDish", x => new { x.DishListID, x.complexDishesID });
                    table.ForeignKey(
                        name: "FK_ComplexDishDish_BaseMenuItem_DishListID",
                        column: x => x.DishListID,
                        principalTable: "BaseMenuItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComplexDishDish_BaseMenuItem_complexDishesID",
                        column: x => x.complexDishesID,
                        principalTable: "BaseMenuItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BaseMenuItemDailyMenu",
                columns: table => new
                {
                    DishesID = table.Column<int>(type: "integer", nullable: false),
                    menusDayID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseMenuItemDailyMenu", x => new { x.DishesID, x.menusDayID });
                    table.ForeignKey(
                        name: "FK_BaseMenuItemDailyMenu_BaseMenuItem_DishesID",
                        column: x => x.DishesID,
                        principalTable: "BaseMenuItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BaseMenuItemDailyMenu_DailyMenus_menusDayID",
                        column: x => x.menusDayID,
                        principalTable: "DailyMenus",
                        principalColumn: "DayID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BaseMenuItemOrder",
                columns: table => new
                {
                    dishesID = table.Column<int>(type: "integer", nullable: false),
                    ordersOrderID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseMenuItemOrder", x => new { x.dishesID, x.ordersOrderID });
                    table.ForeignKey(
                        name: "FK_BaseMenuItemOrder_BaseMenuItem_dishesID",
                        column: x => x.dishesID,
                        principalTable: "BaseMenuItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseMenuItemOrder_Orders_ordersOrderID",
                        column: x => x.ordersOrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseMenuItemDailyMenu_menusDayID",
                table: "BaseMenuItemDailyMenu",
                column: "menusDayID");

            migrationBuilder.CreateIndex(
                name: "IX_BaseMenuItemOrder_ordersOrderID",
                table: "BaseMenuItemOrder",
                column: "ordersOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_ComplexDishDish_complexDishesID",
                table: "ComplexDishDish",
                column: "complexDishesID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseMenuItemDailyMenu");

            migrationBuilder.DropTable(
                name: "BaseMenuItemOrder");

            migrationBuilder.DropTable(
                name: "ComplexDishDish");

            migrationBuilder.DropTable(
                name: "DailyMenus");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "BaseMenuItem");
        }
    }
}
