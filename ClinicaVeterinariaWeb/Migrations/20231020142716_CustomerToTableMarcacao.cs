using Microsoft.EntityFrameworkCore.Migrations;

namespace ClinicaVeterinariaWeb.Migrations
{
    public partial class CustomerToTableMarcacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarcacaoDetails_Clients_ClientId",
                table: "MarcacaoDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_MarcacaoDetails_MarcacaoDetailsTemp_MarcacaoDetailTempId",
                table: "MarcacaoDetails");

            migrationBuilder.DropIndex(
                name: "IX_MarcacaoDetails_ClientId",
                table: "MarcacaoDetails");

            migrationBuilder.DropColumn(
                name: "Cliente",
                table: "Marcacoes");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "MarcacaoDetails");

            migrationBuilder.RenameColumn(
                name: "MarcacaoDetailTempId",
                table: "MarcacaoDetails",
                newName: "ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_MarcacaoDetails_MarcacaoDetailTempId",
                table: "MarcacaoDetails",
                newName: "IX_MarcacaoDetails_ClienteId");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Marcacoes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
                name: "FK_MarcacaoDetails_Clients_ClienteId",
                table: "MarcacaoDetails",
                column: "ClienteId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Marcacoes_Clients_ClienteId",
                table: "Marcacoes",
                column: "ClienteId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarcacaoDetails_Clients_ClienteId",
                table: "MarcacaoDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Marcacoes_Clients_ClienteId",
                table: "Marcacoes");

            migrationBuilder.DropIndex(
                name: "IX_Marcacoes_ClienteId",
                table: "Marcacoes");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Marcacoes");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "MarcacaoDetails",
                newName: "MarcacaoDetailTempId");

            migrationBuilder.RenameIndex(
                name: "IX_MarcacaoDetails_ClienteId",
                table: "MarcacaoDetails",
                newName: "IX_MarcacaoDetails_MarcacaoDetailTempId");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Marcacoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cliente",
                table: "Marcacoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "MarcacaoDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MarcacaoDetails_ClientId",
                table: "MarcacaoDetails",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_MarcacaoDetails_Clients_ClientId",
                table: "MarcacaoDetails",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MarcacaoDetails_MarcacaoDetailsTemp_MarcacaoDetailTempId",
                table: "MarcacaoDetails",
                column: "MarcacaoDetailTempId",
                principalTable: "MarcacaoDetailsTemp",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
