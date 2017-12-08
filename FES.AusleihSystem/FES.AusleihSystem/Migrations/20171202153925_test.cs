using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FES.AusleihSystem.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Geraete_Kategorien_KategorieID",
                table: "Geraete");

            migrationBuilder.DropIndex(
                name: "IX_Geraete_KategorieID",
                table: "Geraete");

            migrationBuilder.DropColumn(
                name: "KategorieID",
                table: "Geraete");

            migrationBuilder.AddColumn<int>(
                name: "GeKategorieID",
                table: "Geraete",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Kategorie",
                table: "Geraete",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Geraete_GeKategorieID",
                table: "Geraete",
                column: "GeKategorieID");

            migrationBuilder.AddForeignKey(
                name: "FK_Geraete_Kategorien_GeKategorieID",
                table: "Geraete",
                column: "GeKategorieID",
                principalTable: "Kategorien",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Geraete_Kategorien_GeKategorieID",
                table: "Geraete");

            migrationBuilder.DropIndex(
                name: "IX_Geraete_GeKategorieID",
                table: "Geraete");

            migrationBuilder.DropColumn(
                name: "GeKategorieID",
                table: "Geraete");

            migrationBuilder.DropColumn(
                name: "Kategorie",
                table: "Geraete");

            migrationBuilder.AddColumn<int>(
                name: "KategorieID",
                table: "Geraete",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Geraete_KategorieID",
                table: "Geraete",
                column: "KategorieID");

            migrationBuilder.AddForeignKey(
                name: "FK_Geraete_Kategorien_KategorieID",
                table: "Geraete",
                column: "KategorieID",
                principalTable: "Kategorien",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
