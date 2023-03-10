// <auto-generated />
using System;
using Bokningsappen.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bokningsappen.Migrations
{
    [DbContext(typeof(MyDBContext))]
    partial class MyDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Bokningsappen.Models.AdminKonto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Lösen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Namn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AdminKonton");
                });

            modelBuilder.Entity("Bokningsappen.Models.Aktivitet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Aktiviteter");
                });

            modelBuilder.Entity("Bokningsappen.Models.Bokning", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("RumId")
                        .HasColumnType("int");

                    b.Property<int?>("SällskapId")
                        .HasColumnType("int");

                    b.Property<bool>("Tillgänglig")
                        .HasColumnType("bit");

                    b.Property<string>("Veckodag")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Veckonummer")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RumId");

                    b.HasIndex("SällskapId");

                    b.ToTable("Bokningar");
                });

            modelBuilder.Entity("Bokningsappen.Models.Rum", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Bord")
                        .HasColumnType("int");

                    b.Property<int>("Stolar")
                        .HasColumnType("int");

                    b.Property<int>("TV")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Bokningsappen.Models.Sällskap", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AktivitetId")
                        .HasColumnType("int");

                    b.Property<int>("Antal_i_sällskapet")
                        .HasColumnType("int");

                    b.Property<string>("Namn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AktivitetId");

                    b.ToTable("Sällskaper");
                });

            modelBuilder.Entity("Bokningsappen.Models.Bokning", b =>
                {
                    b.HasOne("Bokningsappen.Models.Rum", null)
                        .WithMany("Bokningar")
                        .HasForeignKey("RumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bokningsappen.Models.Sällskap", null)
                        .WithMany("Bokningar")
                        .HasForeignKey("SällskapId");
                });

            modelBuilder.Entity("Bokningsappen.Models.Sällskap", b =>
                {
                    b.HasOne("Bokningsappen.Models.Aktivitet", null)
                        .WithMany("Sällskaper")
                        .HasForeignKey("AktivitetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Bokningsappen.Models.Aktivitet", b =>
                {
                    b.Navigation("Sällskaper");
                });

            modelBuilder.Entity("Bokningsappen.Models.Rum", b =>
                {
                    b.Navigation("Bokningar");
                });

            modelBuilder.Entity("Bokningsappen.Models.Sällskap", b =>
                {
                    b.Navigation("Bokningar");
                });
#pragma warning restore 612, 618
        }
    }
}
