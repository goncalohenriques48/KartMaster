﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartMaster.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUtilizador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Utilizadores",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Utilizadores");
        }
    }
}
