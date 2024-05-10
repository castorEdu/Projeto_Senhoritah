﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Senhoritah.API.Context;

#nullable disable

namespace Senhoritah.API.Migrations
{
    [DbContext(typeof(context))]
    [Migration("20240502225300_clients")]
    partial class clients
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Senhoritah.API.Model.ClientsModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<long>("PhoneNumber")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Senhoritah.API.Model.ConfigModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<decimal>("calculo_mercado")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("energia_agua")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("extra")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("mao_de_obra")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("id");

                    b.ToTable("Config");

                    b.HasData(
                        new
                        {
                            id = 1,
                            calculo_mercado = 2m,
                            energia_agua = 20m,
                            extra = 1m,
                            mao_de_obra = 10m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
