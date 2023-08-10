using Microsoft.EntityFrameworkCore.Migrations;

namespace ClinicaVeterinariaWeb.Migrations
{
    public partial class AddClienteTableMarcacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marcacoes_Clients_ClienteId",
                table: "Marcacoes");

            migrationBuilder.DropIndex(
                name: "IX_Marcacoes_ClienteId",
                table: "Marcacoes");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Marcacoes");

            migrationBuilder.AddColumn<string>(
                name: "Cliente",
                table: "Marcacoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cliente",
                table: "Marcacoes");

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Marcacoes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Marcacoes_ClienteId",
                table: "Marcacoes",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Marcacoes_Clients_ClienteId",
                table: "Marcacoes",
                column: "ClienteId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
