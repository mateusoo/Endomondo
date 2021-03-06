// <auto-generated />
using System;
using Endomondo.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Endomondo.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210321161310_AdditionalProperties")]
    partial class AdditionalProperties
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("Endomondo.Models.Journey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("AverageSpeed")
                        .HasColumnType("REAL");

                    b.Property<double>("BurnedCalories")
                        .HasColumnType("REAL");

                    b.Property<double>("Distance")
                        .HasColumnType("REAL");

                    b.Property<long>("Duration")
                        .HasColumnType("INTEGER");

                    b.Property<double>("MaxSpeed")
                        .HasColumnType("REAL");

                    b.Property<int>("NumberOfSteps")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Journeys");
                });

            modelBuilder.Entity("Endomondo.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("JourneyId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Latitude")
                        .HasColumnType("REAL");

                    b.Property<double>("Longitude")
                        .HasColumnType("REAL");

                    b.Property<double>("Speed")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("WriteTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("JourneyId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Endomondo.Models.Location", b =>
                {
                    b.HasOne("Endomondo.Models.Journey", "Journey")
                        .WithMany("Locations")
                        .HasForeignKey("JourneyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Journey");
                });

            modelBuilder.Entity("Endomondo.Models.Journey", b =>
                {
                    b.Navigation("Locations");
                });
#pragma warning restore 612, 618
        }
    }
}
