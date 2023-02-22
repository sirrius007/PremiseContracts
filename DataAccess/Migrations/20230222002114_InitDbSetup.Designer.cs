﻿// <auto-generated />
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(PremiseContractsContext))]
    [Migration("20230222002114_InitDbSetup")]
    partial class InitDbSetup
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataAccess.Models.Contract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EquipmentCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("PremiseCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("Quantity")
                        .HasPrecision(3)
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EquipmentCode");

                    b.HasIndex("PremiseCode");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("DataAccess.Models.Equipment", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<double>("Area")
                        .HasPrecision(10, 2)
                        .HasColumnType("float(10)");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Code");

                    b.ToTable("Equipment");

                    b.HasData(
                        new
                        {
                            Code = "EQ00000001",
                            Area = 50.0,
                            Id = 0,
                            Name = "Equipment A"
                        },
                        new
                        {
                            Code = "EQ00000002",
                            Area = 10.0,
                            Id = 0,
                            Name = "Equipment B"
                        },
                        new
                        {
                            Code = "EQ00000003",
                            Area = 25.0,
                            Id = 0,
                            Name = "Equipment C"
                        });
                });

            modelBuilder.Entity("DataAccess.Models.Premise", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<double>("Area")
                        .HasPrecision(10, 2)
                        .HasColumnType("float(10)");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Code");

                    b.ToTable("Premises");

                    b.HasData(
                        new
                        {
                            Code = "ABCDE00001",
                            Area = 500.0,
                            Id = 0,
                            Name = "Premise A"
                        },
                        new
                        {
                            Code = "ABCDE00002",
                            Area = 400.0,
                            Id = 0,
                            Name = "Premise B"
                        },
                        new
                        {
                            Code = "ABCDE00003",
                            Area = 1000.0,
                            Id = 0,
                            Name = "Premise C"
                        },
                        new
                        {
                            Code = "ABCDE00004",
                            Area = 700.0,
                            Id = 0,
                            Name = "Premise D"
                        },
                        new
                        {
                            Code = "ABCDE00005",
                            Area = 800.0,
                            Id = 0,
                            Name = "Premise E"
                        },
                        new
                        {
                            Code = "ABCDE00006",
                            Area = 100.0,
                            Id = 0,
                            Name = "Premise F"
                        },
                        new
                        {
                            Code = "ABCDE00007",
                            Area = 300.0,
                            Id = 0,
                            Name = "Premise G"
                        });
                });

            modelBuilder.Entity("DataAccess.Models.Contract", b =>
                {
                    b.HasOne("DataAccess.Models.Equipment", "Equipment")
                        .WithMany("Contracts")
                        .HasForeignKey("EquipmentCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Models.Premise", "Premise")
                        .WithMany("Contracts")
                        .HasForeignKey("PremiseCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Equipment");

                    b.Navigation("Premise");
                });

            modelBuilder.Entity("DataAccess.Models.Equipment", b =>
                {
                    b.Navigation("Contracts");
                });

            modelBuilder.Entity("DataAccess.Models.Premise", b =>
                {
                    b.Navigation("Contracts");
                });
#pragma warning restore 612, 618
        }
    }
}
