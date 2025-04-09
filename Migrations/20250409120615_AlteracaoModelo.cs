using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartMaster.Migrations
{
    /// <inheritdoc />
    public partial class AlteracaoModelo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Participacoes",
                table: "Participacoes");

            migrationBuilder.DropIndex(
                name: "IX_Participacoes_UtilizadorId",
                table: "Participacoes");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Utilizadores");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Utilizadores");

            migrationBuilder.DropColumn(
                name: "ParticipacaoId",
                table: "Participacoes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Participacoes",
                table: "Participacoes",
                columns: new[] { "UtilizadorId", "CorridaId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Participacoes",
                table: "Participacoes");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Utilizadores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Utilizadores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ParticipacaoId",
                table: "Participacoes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Participacoes",
                table: "Participacoes",
                column: "ParticipacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Participacoes_UtilizadorId",
                table: "Participacoes",
                column: "UtilizadorId");
        }
    }
}
