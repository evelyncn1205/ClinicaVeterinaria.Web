using Microsoft.EntityFrameworkCore.Migrations;

namespace ClinicaVeterinariaWeb.Migrations
{
    public partial class AddTipoConsulta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MotivoDaConsulta",
                table: "MarcacaoDetailsTemp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MotivoDaConsulta",
                table: "MarcacaoDetailsTemp",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
