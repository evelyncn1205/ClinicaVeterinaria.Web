using Microsoft.EntityFrameworkCore.Migrations;

namespace ClinicaVeterinariaWeb.Migrations
{
    public partial class AddQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarcacaoDetails_AspNetUsers_UserId",
                table: "MarcacaoDetails");

            migrationBuilder.DropIndex(
                name: "IX_MarcacaoDetails_UserId",
                table: "MarcacaoDetails");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MarcacaoDetails");

            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "Marcacoes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "MarcacaoDetailsTemp",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Marcacoes");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "MarcacaoDetailsTemp");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "MarcacaoDetails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MarcacaoDetails_UserId",
                table: "MarcacaoDetails",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MarcacaoDetails_AspNetUsers_UserId",
                table: "MarcacaoDetails",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
