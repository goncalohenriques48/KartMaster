using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartMaster.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDuracaoToReserva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duracao",
                table: "Reservas",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duracao",
                table: "Reservas");
        }
    }
}
