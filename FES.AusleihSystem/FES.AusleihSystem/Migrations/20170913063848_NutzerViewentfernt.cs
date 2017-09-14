using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FES.AusleihSystem.Migrations
{
    public partial class NutzerViewentfernt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservierungen_Nutzer_NutzerID",
                table: "Reservierungen");

            migrationBuilder.DropIndex(
                name: "IX_Reservierungen_NutzerID",
                table: "Reservierungen");

            migrationBuilder.DropColumn(
                name: "NutzerID",
                table: "Reservierungen");

            migrationBuilder.DropTable(
                name: "Nutzer");

            migrationBuilder.DropTable(
                name: "Rolle");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rolle",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rolle", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Nutzer",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    NutzerRolleID = table.Column<int>(nullable: true),
                    Passwort = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nutzer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Nutzer_Rolle_NutzerRolleID",
                        column: x => x.NutzerRolleID,
                        principalTable: "Rolle",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.AddColumn<int>(
                name: "NutzerID",
                table: "Reservierungen",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservierungen_NutzerID",
                table: "Reservierungen",
                column: "NutzerID");

            migrationBuilder.CreateIndex(
                name: "IX_Nutzer_NutzerRolleID",
                table: "Nutzer",
                column: "NutzerRolleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservierungen_Nutzer_NutzerID",
                table: "Reservierungen",
                column: "NutzerID",
                principalTable: "Nutzer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
