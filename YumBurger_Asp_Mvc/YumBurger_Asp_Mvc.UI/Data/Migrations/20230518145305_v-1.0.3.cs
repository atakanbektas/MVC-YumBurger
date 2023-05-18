using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_Hamburger.Data.Migrations
{
    public partial class v103 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSellable",
                table: "Extras",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSellable",
                table: "Extras");
        }
    }
}
