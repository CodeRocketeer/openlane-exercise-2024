﻿// <auto-generated />
using System;
using BiddingSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BiddingSystem.Infrastructure.Migrations
{
    [DbContext(typeof(BiddingDbContext))]
    [Migration("20241120121438_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("BiddingSystem.Domain.Models.Bid", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double>("Amount")
                        .HasColumnType("REAL");

                    b.Property<string>("BidderId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ItemId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Timestamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("BidderId")
                        .HasDatabaseName("IX_Bid_BidderId");

                    b.HasIndex("ItemId")
                        .HasDatabaseName("IX_Bid_ItemId");

                    b.ToTable("Bids");
                });
#pragma warning restore 612, 618
        }
    }
}
