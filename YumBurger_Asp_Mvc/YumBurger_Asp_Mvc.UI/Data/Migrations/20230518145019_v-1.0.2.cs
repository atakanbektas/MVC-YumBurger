using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_Hamburger.Data.Migrations
{
    public partial class v102 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CokePatotoSize",
                table: "Menus");

            migrationBuilder.AddColumn<bool>(
                name: "IsSellable",
                table: "Menus",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSellable",
                table: "Menus");

            migrationBuilder.AddColumn<int>(
                name: "CokePatotoSize",
                table: "Menus",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);
        }
    }
}
