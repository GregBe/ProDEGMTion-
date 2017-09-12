using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using FES.AusleihSystem.ViewModels;

namespace FES.AusleihSystem.Migrations
{
    public partial class GeraeteReservierung : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Geraete_Reservierungen_ReservierungViewModelReservierungsNummer",
                table: "Geraete");

            migrationBuilder.DropIndex(
                name: "IX_Geraete_ReservierungViewModelReservierungsNummer",
                table: "Geraete");

            migrationBuilder.DropColumn(
                name: "ReservierungViewModelReservierungsNummer",
                table: "Geraete");

            migrationBuilder.AddColumn<int>(
                name: "GeraeteStatus",
                table: "Geraete",
                nullable: false,
                defaultValue: GeraetViewModel.Status.isVerfugbar);

            migrationBuilder.AddColumn<int>(
                name: "ReservierungsNummer",
                table: "Geraete",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Geraete_ReservierungsNummer",
                table: "Geraete",
                column: "ReservierungsNummer");

            migrationBuilder.AddForeignKey(
                name: "FK_Geraete_Reservierungen_ReservierungsNummer",
                table: "Geraete",
                column: "ReservierungsNummer",
                principalTable: "Reservierungen",
                principalColumn: "ReservierungsNummer",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Geraete_Reservierungen_ReservierungsNummer",
                table: "Geraete");

            migrationBuilder.DropIndex(
                name: "IX_Geraete_ReservierungsNummer",
                table: "Geraete");

            migrationBuilder.DropColumn(
                name: "GeraeteStatus",
                table: "Geraete");

            migrationBuilder.DropColumn(
                name: "ReservierungsNummer",
                table: "Geraete");

            migrationBuilder.AddColumn<int>(
                name: "ReservierungViewModelReservierungsNummer",
                table: "Geraete",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Geraete_ReservierungViewModelReservierungsNummer",
                table: "Geraete",
                column: "ReservierungViewModelReservierungsNummer");

            migrationBuilder.AddForeignKey(
                name: "FK_Geraete_Reservierungen_ReservierungViewModelReservierungsNummer",
                table: "Geraete",
                column: "ReservierungViewModelReservierungsNummer",
                principalTable: "Reservierungen",
                principalColumn: "ReservierungsNummer",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
