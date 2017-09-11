using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FES.AusleihSystem.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Reservierungen",
                columns: table => new
                {
                    ReservierungsNummer = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NutzerID = table.Column<int>(nullable: true),
                    ReservierungsBeginn = table.Column<DateTime>(nullable: false),
                    ReservierungsDauer = table.Column<TimeSpan>(nullable: false),
                    ReservierungsEnde = table.Column<DateTime>(nullable: false),
                    ReservierungsZeitpunkt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservierungen", x => x.ReservierungsNummer);
                    table.ForeignKey(
                        name: "FK_Reservierungen_Nutzer_NutzerID",
                        column: x => x.NutzerID,
                        principalTable: "Nutzer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.AddColumn<int>(
                name: "ReservierungViewModelReservierungsNummer",
                table: "Geraete",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Geraete_ReservierungViewModelReservierungsNummer",
                table: "Geraete",
                column: "ReservierungViewModelReservierungsNummer");

            migrationBuilder.CreateIndex(
                name: "IX_Nutzer_NutzerRolleID",
                table: "Nutzer",
                column: "NutzerRolleID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservierungen_NutzerID",
                table: "Reservierungen",
                column: "NutzerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Geraete_Reservierungen_ReservierungViewModelReservierungsNummer",
                table: "Geraete",
                column: "ReservierungViewModelReservierungsNummer",
                principalTable: "Reservierungen",
                principalColumn: "ReservierungsNummer",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropTable(
                name: "Reservierungen");

            migrationBuilder.DropTable(
                name: "Nutzer");

            migrationBuilder.DropTable(
                name: "Rolle");
        }
    }
}
