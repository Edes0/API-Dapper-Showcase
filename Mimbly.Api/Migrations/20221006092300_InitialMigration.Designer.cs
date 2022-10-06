﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mimbly.Infrastructure.Identity.Context;

#nullable disable

namespace Mimbly.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221006092300_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Mimbly.Domain.Enitites.Mimbox", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id")
                        .HasColumnOrder(1);

                    b.Property<byte>("Age")
                        .HasColumnType("TINYINT")
                        .HasColumnName("age");

                    b.Property<string>("FirstName")
                        .HasColumnType("Char(108)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasColumnType("Char(108)")
                        .HasColumnName("last_name");

                    b.HasKey("Id");

                    b.HasIndex("Age");

                    b.ToTable("Mimbox");
                });

            modelBuilder.Entity("Mimbly.Domain.Enitites.RefreshToken", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("CHAR(36)")
                        .HasColumnName("id")
                        .HasColumnOrder(1);

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("Char(255)")
                        .HasColumnName("refresh_token");

                    b.Property<DateTime>("TokenSetAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("token_set_at");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("Char(36)")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("Token", "UserId");

                    b.ToTable("refresh_token");
                });

            modelBuilder.Entity("Mimbly.Domain.Enitites.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("CHAR(36)")
                        .HasColumnName("id")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("CHAR(128)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .HasColumnType("CHAR(128)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasColumnType("CHAR(128)")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("CHAR(255)")
                        .HasColumnName("password");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("user");
                });

            modelBuilder.Entity("Mimbly.Domain.Enitites.RefreshToken", b =>
                {
                    b.HasOne("Mimbly.Domain.Enitites.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
