using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FES.AusleihSystem.Migrations
{
    public partial class ReservierungsNutzerUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NutzerId",
                table: "Reservierungen",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservierungen_NutzerId",
                table: "Reservierungen",
                column: "NutzerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservierungen_AspNetUsers_NutzerId",
                table: "Reservierungen",
                column: "NutzerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservierungen_AspNetUsers_NutzerId",
                table: "Reservierungen");

            migrationBuilder.DropIndex(
                name: "IX_Reservierungen_NutzerId",
                table: "Reservierungen");

            migrationBuilder.DropColumn(
                name: "NutzerId",
                table: "Reservierungen");
        }
    }
}
