using Microsoft.EntityFrameworkCore.Migrations;

namespace ClinicaVeterinariaWeb.Migrations
{
    public partial class AddSubjectAndClienteToContacto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Contactos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Contactos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Name",
                table: "Countries",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contactos_ClienteId",
                table: "Contactos",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contactos_Clients_ClienteId",
                table: "Contactos",
                column: "ClienteId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contactos_Clients_ClienteId",
                table: "Contactos");

            migrationBuilder.DropIndex(
                name: "IX_Countries_Name",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Contactos_ClienteId",
                table: "Contactos");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Contactos");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Contactos");
        }
    }
}
