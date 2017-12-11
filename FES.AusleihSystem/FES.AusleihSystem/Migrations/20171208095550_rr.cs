using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using FES.AusleihSystem.ViewModels;

namespace FES.AusleihSystem.Migrations
{
    public partial class rr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AusgeliehenStatus",
                table: "Geraete",
                nullable: false,
                defaultValue: GeraetViewModel.EntliehenStatus.isAusgeliehen);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AusgeliehenStatus",
                table: "Geraete");
        }
    }
}
