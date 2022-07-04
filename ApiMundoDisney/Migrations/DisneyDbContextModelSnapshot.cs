﻿// <auto-generated />
using System;
using ApiMundoDisney.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApiMundoDisney.Migrations
{
    [DbContext(typeof(DisneyDbContext))]
    partial class DisneyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApiMundoDisney.Models.Genero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Imagen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Generos");
                });

            modelBuilder.Entity("ApiMundoDisney.Models.PeliculaSerie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Calificacion")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Imagen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PeliculasSeries");
                });

            modelBuilder.Entity("ApiMundoDisney.Models.Personaje", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Edad")
                        .HasColumnType("int");

                    b.Property<string>("Historia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Imagen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Peso")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Personajes");
                });

            modelBuilder.Entity("ApiMundoDisney.Models.Personaje_PeSe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PeliculaSerieId")
                        .HasColumnType("int");

                    b.Property<int>("PersonajeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PeliculaSerieId");

                    b.HasIndex("PersonajeId");

                    b.ToTable("Personajes_PeSe");
                });

            modelBuilder.Entity("ApiMundoDisney.Models.Personaje_PeSe", b =>
                {
                    b.HasOne("ApiMundoDisney.Models.PeliculaSerie", "PeliculasSeries")
                        .WithMany("PersonajesPeliculasSeries")
                        .HasForeignKey("PeliculaSerieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiMundoDisney.Models.Personaje", "Personajes")
                        .WithMany("PersonajesPeliculasSeries")
                        .HasForeignKey("PersonajeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PeliculasSeries");

                    b.Navigation("Personajes");
                });

            modelBuilder.Entity("ApiMundoDisney.Models.PeliculaSerie", b =>
                {
                    b.Navigation("PersonajesPeliculasSeries");
                });

            modelBuilder.Entity("ApiMundoDisney.Models.Personaje", b =>
                {
                    b.Navigation("PersonajesPeliculasSeries");
                });
#pragma warning restore 612, 618
        }
    }
}
