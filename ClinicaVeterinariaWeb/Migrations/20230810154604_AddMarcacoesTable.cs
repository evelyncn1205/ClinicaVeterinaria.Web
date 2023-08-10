using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClinicaVeterinariaWeb.Migrations
{
    public partial class AddMarcacoesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MarcacaoId",
                table: "MarcacaoDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Marcacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(type: "int", nullable: true),
                    NomeAnimal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hora = table.Column<TimeSpan>(type: "time", nullable: false),
                    TipodaConsulta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CellPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marcacoes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Marcacoes_Clients_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarcacaoDetails_MarcacaoId",
                table: "MarcacaoDetails",
                column: "MarcacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Marcacoes_ClienteId",
                table: "Marcacoes",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Marcacoes_UserId",
                table: "Marcacoes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MarcacaoDetails_Marcacoes_MarcacaoId",
                table: "MarcacaoDetails",
                column: "MarcacaoId",
                principalTable: "Marcacoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarcacaoDetails_Marcacoes_MarcacaoId",
                table: "MarcacaoDetails");

            migrationBuilder.DropTable(
                name: "Marcacoes");

            migrationBuilder.DropIndex(
                name: "IX_MarcacaoDetails_MarcacaoId",
                table: "MarcacaoDetails");

            migrationBuilder.DropColumn(
                name: "MarcacaoId",
                table: "MarcacaoDetails");
        }
    }
}
