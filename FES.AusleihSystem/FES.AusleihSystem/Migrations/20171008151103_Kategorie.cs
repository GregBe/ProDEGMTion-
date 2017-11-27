using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FES.AusleihSystem.Migrations
{
    public partial class Kategorie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategorien",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorien", x => x.ID);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropTable(
                name: "Kategorien");
        }
    }
}
