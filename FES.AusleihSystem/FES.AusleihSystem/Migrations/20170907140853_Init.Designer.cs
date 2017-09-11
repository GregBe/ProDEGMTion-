using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using FES.AusleihSystem.Data;

namespace FES.AusleihSystem.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170907140853_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FES.AusleihSystem.ViewModels.GeraeteViewModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EAN");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Geraete");
                });
        }
    }
}
