﻿// <auto-generated />
using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(ApiContext))]
    [Migration("20240331115629_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Infrastructure.Entities.SubEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("AdvertisingUpdate")
                        .HasColumnType("bit");

                    b.Property<bool>("DailyNewsLetter")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EventUpDates")
                        .HasColumnType("bit");

                    b.Property<bool>("PodCasts")
                        .HasColumnType("bit");

                    b.Property<bool>("StartupsWeekly")
                        .HasColumnType("bit");

                    b.Property<bool>("WeekInReview")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("SubEntities");
                });
#pragma warning restore 612, 618
        }
    }
}
