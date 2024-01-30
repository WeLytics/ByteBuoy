﻿// <auto-generated />
using System;
using ByteBuoy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ByteBuoy.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ByteBuoyDbContext))]
    partial class ByteBuoyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.1");

            modelBuilder.Entity("ByteBuoy.Domain.Entities.Incident", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("TEXT");

                    b.Property<int>("PageId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PageId");

                    b.ToTable("Incidents");
                });

            modelBuilder.Entity("ByteBuoy.Domain.Entities.JobRun", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("JobFinished")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("JobStarted")
                        .HasColumnType("TEXT");

                    b.Property<string>("JobStatus")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("JobRuns");
                });

            modelBuilder.Entity("ByteBuoy.Domain.Entities.Metric", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("TEXT");

                    b.Property<string>("MetaJson")
                        .HasColumnType("TEXT");

                    b.Property<int?>("MetricGroupId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PageId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("Value")
                        .HasColumnType("TEXT");

                    b.Property<string>("ValueString")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MetricGroupId");

                    b.HasIndex("PageId");

                    b.ToTable("Metrics");
                });

            modelBuilder.Entity("ByteBuoy.Domain.Entities.MetricGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("PageId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PageId");

                    b.ToTable("MetricGroups");
                });

            modelBuilder.Entity("ByteBuoy.Domain.Entities.Page", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Slug")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("ByteBuoy.Domain.Entities.Incident", b =>
                {
                    b.HasOne("ByteBuoy.Domain.Entities.Page", "Page")
                        .WithMany()
                        .HasForeignKey("PageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Page");
                });

            modelBuilder.Entity("ByteBuoy.Domain.Entities.Metric", b =>
                {
                    b.HasOne("ByteBuoy.Domain.Entities.MetricGroup", "MetricGroup")
                        .WithMany()
                        .HasForeignKey("MetricGroupId");

                    b.HasOne("ByteBuoy.Domain.Entities.Page", "Page")
                        .WithMany()
                        .HasForeignKey("PageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MetricGroup");

                    b.Navigation("Page");
                });

            modelBuilder.Entity("ByteBuoy.Domain.Entities.MetricGroup", b =>
                {
                    b.HasOne("ByteBuoy.Domain.Entities.Page", "Page")
                        .WithMany()
                        .HasForeignKey("PageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Page");
                });
#pragma warning restore 612, 618
        }
    }
}
