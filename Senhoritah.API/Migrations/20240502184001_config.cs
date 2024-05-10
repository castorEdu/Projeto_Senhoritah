using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Senhoritah.API.Migrations
{
    public partial class config : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Config",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mao_de_obra = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    energia_agua = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    extra = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    calculo_mercado = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Config", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "Config",
                columns: new[] { "id", "calculo_mercado", "energia_agua", "extra", "mao_de_obra" },
                values: new object[] { 1, 2m, 20m, 1m, 10m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Config");
        }
    }
}
