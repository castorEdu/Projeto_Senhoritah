using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Senhoritah.API.Migrations
{
    public partial class updateModel3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "decimal(18,2)",
                table: "Products",
                newName: "Amount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Products",
                newName: "decimal(18,2)");
        }
    }
}
