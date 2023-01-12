﻿// <auto-generated />
using System;
using EF_Demo_many2many2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bokningsappen.Migrations
{
    [DbContext(typeof(MyDBContext))]
    [Migration("20230112102846_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Bokningsappen.Models.Bokning", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Antal_personer")
                        .HasColumnType("int");

                    b.Property<string>("Namn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rum")
                        .HasColumnType("int");

                    b.Property<string>("Veckodag")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Veckonummer")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Bokningar");
                });
#pragma warning restore 612, 618
        }
    }
}
