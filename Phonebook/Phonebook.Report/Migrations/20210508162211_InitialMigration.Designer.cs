// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Phonebook.Report.Entity;

namespace Phonebook.Report.Migrations
{
    [DbContext(typeof(ReportDbContext))]
    [Migration("20210508162211_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Phonebook.Report.Entity.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<int>("ReportDetailId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ReportDetailId")
                        .IsUnique();

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Phonebook.Report.Entity.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("ReportDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("ReportStatus")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("Phonebook.Report.Entity.ReportDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("PersonCount")
                        .HasColumnType("integer");

                    b.Property<int>("PhoneCount")
                        .HasColumnType("integer");

                    b.Property<int>("ReportId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ReportId")
                        .IsUnique();

                    b.ToTable("ReportDetails");
                });

            modelBuilder.Entity("Phonebook.Report.Entity.Location", b =>
                {
                    b.HasOne("Phonebook.Report.Entity.ReportDetail", "ReportDetail")
                        .WithOne("Location")
                        .HasForeignKey("Phonebook.Report.Entity.Location", "ReportDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ReportDetail");
                });

            modelBuilder.Entity("Phonebook.Report.Entity.ReportDetail", b =>
                {
                    b.HasOne("Phonebook.Report.Entity.Report", "Report")
                        .WithOne("ReportDetail")
                        .HasForeignKey("Phonebook.Report.Entity.ReportDetail", "ReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("Phonebook.Report.Entity.Report", b =>
                {
                    b.Navigation("ReportDetail");
                });

            modelBuilder.Entity("Phonebook.Report.Entity.ReportDetail", b =>
                {
                    b.Navigation("Location");
                });
#pragma warning restore 612, 618
        }
    }
}
