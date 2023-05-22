using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_Hamburger.Data.Migrations
{
    public partial class v106 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrdersMenus",
                table: "OrdersMenus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrdersExtras",
                table: "OrdersExtras");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OrdersMenus",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OrdersExtras",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrdersMenus",
                table: "OrdersMenus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrdersExtras",
                table: "OrdersExtras",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersMenus_OrderId",
                table: "OrdersMenus",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersExtras_OrderId",
                table: "OrdersExtras",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrdersMenus",
                table: "OrdersMenus");

            migrationBuilder.DropIndex(
                name: "IX_OrdersMenus_OrderId",
                table: "OrdersMenus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrdersExtras",
                table: "OrdersExtras");

            migrationBuilder.DropIndex(
                name: "IX_OrdersExtras_OrderId",
                table: "OrdersExtras");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrdersMenus");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrdersExtras");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrdersMenus",
                table: "OrdersMenus",
                columns: new[] { "OrderId", "MenuId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrdersExtras",
                table: "OrdersExtras",
                columns: new[] { "OrderId", "ExtraId" });
        }
    }
}
