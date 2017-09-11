using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using FES.AusleihSystem.Data;

namespace FES.AusleihSystem.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FES.AusleihSystem.Models.Rolle", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Role");

                    b.HasKey("ID");

                    b.ToTable("Rolle");
                });

            modelBuilder.Entity("FES.AusleihSystem.ViewModels.GeraetViewModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EAN");

                    b.Property<string>("Name");

                    b.Property<int?>("ReservierungViewModelReservierungsNummer");

                    b.HasKey("ID");

                    b.HasIndex("ReservierungViewModelReservierungsNummer");

                    b.ToTable("Geraete");
                });

            modelBuilder.Entity("FES.AusleihSystem.ViewModels.NutzerViewModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<int?>("NutzerRolleID");

                    b.Property<string>("Passwort");

                    b.HasKey("ID");

                    b.HasIndex("NutzerRolleID");

                    b.ToTable("Nutzer");
                });

            modelBuilder.Entity("FES.AusleihSystem.ViewModels.ReservierungViewModel", b =>
                {
                    b.Property<int>("ReservierungsNummer")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("NutzerID");

                    b.Property<DateTime>("ReservierungsBeginn");

                    b.Property<TimeSpan>("ReservierungsDauer");

                    b.Property<DateTime>("ReservierungsEnde");

                    b.Property<DateTime>("ReservierungsZeitpunkt");

                    b.HasKey("ReservierungsNummer");

                    b.HasIndex("NutzerID");

                    b.ToTable("Reservierungen");
                });

            modelBuilder.Entity("FES.AusleihSystem.ViewModels.GeraetViewModel", b =>
                {
                    b.HasOne("FES.AusleihSystem.ViewModels.ReservierungViewModel")
                        .WithMany("GeraeteListe")
                        .HasForeignKey("ReservierungViewModelReservierungsNummer");
                });

            modelBuilder.Entity("FES.AusleihSystem.ViewModels.NutzerViewModel", b =>
                {
                    b.HasOne("FES.AusleihSystem.Models.Rolle", "NutzerRolle")
                        .WithMany()
                        .HasForeignKey("NutzerRolleID");
                });

            modelBuilder.Entity("FES.AusleihSystem.ViewModels.ReservierungViewModel", b =>
                {
                    b.HasOne("FES.AusleihSystem.ViewModels.NutzerViewModel", "Nutzer")
                        .WithMany()
                        .HasForeignKey("NutzerID");
                });
        }
    }
}
