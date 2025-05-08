using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartMaster.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentityUserToUtilizador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "Utilizadores",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Utilizadores_IdentityUserId",
                table: "Utilizadores",
                column: "IdentityUserId",
                unique: true,
                filter: "[IdentityUserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Utilizadores_AspNetUsers_IdentityUserId",
                table: "Utilizadores",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Utilizadores_AspNetUsers_IdentityUserId",
                table: "Utilizadores");

            migrationBuilder.DropIndex(
                name: "IX_Utilizadores_IdentityUserId",
                table: "Utilizadores");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "Utilizadores");
        }
    }
}
