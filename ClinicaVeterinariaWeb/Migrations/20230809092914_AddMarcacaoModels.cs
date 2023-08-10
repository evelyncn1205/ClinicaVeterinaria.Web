using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClinicaVeterinariaWeb.Migrations
{
    public partial class AddMarcacaoModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comunicacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mensagem = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comunicacoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarcacaoDetailsTemp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    NomeAnimal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CellPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipodaConsulta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hora = table.Column<TimeSpan>(type: "time", nullable: false),
                    MotivoDaConsulta = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarcacaoDetailsTemp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarcacaoDetailsTemp_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MarcacaoDetailsTemp_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MarcacaoDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    NomeAnimal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CellPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipodaConsulta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hora = table.Column<TimeSpan>(type: "time", nullable: false),
                    MotivoDaConsulta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    MarcacaoDetailTempId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarcacaoDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarcacaoDetails_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MarcacaoDetails_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MarcacaoDetails_MarcacaoDetailsTemp_MarcacaoDetailTempId",
                        column: x => x.MarcacaoDetailTempId,
                        principalTable: "MarcacaoDetailsTemp",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarcacaoDetails_ClientId",
                table: "MarcacaoDetails",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_MarcacaoDetails_MarcacaoDetailTempId",
                table: "MarcacaoDetails",
                column: "MarcacaoDetailTempId");

            migrationBuilder.CreateIndex(
                name: "IX_MarcacaoDetails_UserId",
                table: "MarcacaoDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MarcacaoDetailsTemp_ClientId",
                table: "MarcacaoDetailsTemp",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_MarcacaoDetailsTemp_UserId",
                table: "MarcacaoDetailsTemp",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comunicacoes");

            migrationBuilder.DropTable(
                name: "MarcacaoDetails");

            migrationBuilder.DropTable(
                name: "MarcacaoDetailsTemp");
        }
    }
}
