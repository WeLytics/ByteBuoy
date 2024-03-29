﻿// <auto-generated />
using System;
using ByteBuoy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ByteBuoy.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ByteBuoyDbContext))]
    [Migration("20240209200801_RemovedDeletedMetricColumn")]
    partial class RemovedDeletedMetricColumn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("ByteBuoy.Domain.Entities.Job", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FinishedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("HostName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("StartedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("ByteBuoy.Domain.Entities.JobHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("TEXT");

                    b.Property<int>("JobId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TaskName")
                        .HasColumnType("TEXT");

                    b.Property<int>("TaskNumber")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("JobHistory");
                });

            modelBuilder.Entity("ByteBuoy.Domain.Entities.Metric", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
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
