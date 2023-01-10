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
    [Migration("20230110104638_AddTotalWashesAndTotalTap")]
    partial class AddTotalWashesAndTotalTap
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Mimbly.Domain.Entities.AzureEvents.EventLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime")
                        .HasColumnName("Created");

                    b.Property<string>("Log")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Log");

                    b.Property<Guid>("MimboxId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Mimbox_Id");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Type");

                    b.HasKey("Id");

                    b.HasIndex("MimboxId");

                    b.ToTable("Event_Log");
                });

            modelBuilder.Entity("Mimbly.Domain.Entities.AzureEvents.MimboxErrorLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime")
                        .HasColumnName("Created");

                    b.Property<bool>("Discarded")
                        .HasColumnType("bit")
                        .HasColumnName("Discarded");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Message");

                    b.Property<Guid>("MimboxId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Mimbox_Id");

                    b.Property<string>("Severity")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Severity");

                    b.HasKey("Id");

                    b.HasIndex("MimboxId");

                    b.ToTable("Mimbox_Error_Log");
                });

            modelBuilder.Entity("Mimbly.Domain.Entities.AzureEvents.WashStats", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id")
                        .HasColumnOrder(1);

                    b.Property<double>("Co2Saved")
                        .HasColumnType("float")
                        .HasColumnName("Co2_saved");

                    b.Property<double>("EconomySaved")
                        .HasColumnType("float")
                        .HasColumnName("Economy_saved");

                    b.Property<DateTime?>("EndedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Ended_At");

                    b.Property<Guid>("MimboxId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Mimbox_Id");

                    b.Property<double>("PlasticSaved")
                        .HasColumnType("float")
                        .HasColumnName("Plastic_saved");

                    b.Property<DateTime>("StartedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Started_At");

                    b.Property<byte>("WashingMachineId")
                        .HasColumnType("tinyint")
                        .HasColumnName("Washing_Machine_Id");

                    b.Property<double>("WaterSaved")
                        .HasColumnType("float")
                        .HasColumnName("Water_saved");

                    b.HasKey("Id");

                    b.HasIndex("MimboxId");

                    b.ToTable("Wash_Stats");
                });

            modelBuilder.Entity("Mimbly.Domain.Entities.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id")
                        .HasColumnOrder(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("Nvarchar(50)")
                        .HasColumnName("Name");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Parent_Id");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("Mimbly.Domain.Entities.CompanyContact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id")
                        .HasColumnOrder(1);

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Company_Id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("Varchar(100)")
                        .HasColumnName("Email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("Nvarchar(50)")
                        .HasColumnName("First_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("Nvarchar(50)")
                        .HasColumnName("Last_name");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("Varchar(15)")
                        .HasColumnName("Phone_number");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("Nvarchar(50)")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Company_Contact");
                });

            modelBuilder.Entity("Mimbly.Domain.Entities.Mimbox", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id")
                        .HasColumnOrder(1);

                    b.Property<double>("Co2Saved")
                        .HasColumnType("float")
                        .HasColumnName("Co2_Saved");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Company_Id");

                    b.Property<double>("EconomySaved")
                        .HasColumnType("float")
                        .HasColumnName("Economy_Saved");

                    b.Property<Guid?>("LocationId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Mimbox_Location_Id");

                    b.Property<Guid>("ModelId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Mimbox_Model_Id");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("Nvarchar(50)")
                        .HasColumnName("Nickname");

                    b.Property<double>("PlasticSaved")
                        .HasColumnType("float")
                        .HasColumnName("Plastic_Saved");

                    b.Property<DateTime>("StatsUpdatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Stats_Updated_At");

                    b.Property<Guid>("StatusId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Mimbox_Status_Id");

                    b.Property<int>("TotalTap")
                        .HasColumnType("int")
                        .HasColumnName("Total_Tap");

                    b.Property<int>("TotalWashes")
                        .HasColumnType("int")
                        .HasColumnName("Total_Washes");

                    b.Property<double>("WaterSaved")
                        .HasColumnType("float")
                        .HasColumnName("Water_Saved");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("LocationId");

                    b.HasIndex("ModelId");

                    b.HasIndex("StatusId");

                    b.ToTable("Mimbox");
                });

            modelBuilder.Entity("Mimbly.Domain.Entities.MimboxContact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id")
                        .HasColumnOrder(1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("Varchar(100)")
                        .HasColumnName("Email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("Nvarchar(50)")
                        .HasColumnName("First_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("Nvarchar(50)")
                        .HasColumnName("Last_name");

                    b.Property<Guid>("MimboxId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Mimbox_Id");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("Varchar(15)")
                        .HasColumnName("Phone_number");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("Nvarchar(50)")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.HasIndex("MimboxId");

                    b.ToTable("Mimbox_Contact");
                });

            modelBuilder.Entity("Mimbly.Domain.Entities.MimboxLocation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id")
                        .HasColumnOrder(1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("Nvarchar(100)")
                        .HasColumnName("City");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("Nvarchar(100)")
                        .HasColumnName("Country");

                    b.Property<string>("PostalCode")
                        .HasColumnType("Varchar(5)")
                        .HasColumnName("Postal_code");

                    b.Property<string>("Region")
                        .HasColumnType("Nvarchar(100)")
                        .HasColumnName("Region");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasColumnType("Nvarchar(100)")
                        .HasColumnName("Street_address");

                    b.HasKey("Id");

                    b.ToTable("Mimbox_Location");
                });

            modelBuilder.Entity("Mimbly.Domain.Entities.MimboxLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Created_At");

                    b.Property<string>("Log")
                        .IsRequired()
                        .HasColumnType("Nvarchar(max)")
                        .HasColumnName("Log");

                    b.Property<Guid>("MimboxId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Mimbox_Id");

                    b.HasKey("Id");

                    b.HasIndex("MimboxId");

                    b.ToTable("Mimbox_Log");
                });

            modelBuilder.Entity("Mimbly.Domain.Entities.MimboxModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id")
                        .HasColumnOrder(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("Nvarchar(50)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Mimbox_Model");
                });

            modelBuilder.Entity("Mimbly.Domain.Entities.MimboxStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id")
                        .HasColumnOrder(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("Nvarchar(50)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Mimbox_Status");
                });

            modelBuilder.Entity("Mimbly.Domain.Entities.AzureEvents.EventLog", b =>
                {
                    b.HasOne("Mimbly.Domain.Entities.Mimbox", null)
                        .WithMany("EventLogList")
                        .HasForeignKey("MimboxId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Mimbly.Domain.Entities.AzureEvents.MimboxErrorLog", b =>
                {
                    b.HasOne("Mimbly.Domain.Entities.Mimbox", null)
                        .WithMany("ErrorLogList")
                        .HasForeignKey("MimboxId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Mimbly.Domain.Entities.AzureEvents.WashStats", b =>
                {
                    b.HasOne("Mimbly.Domain.Entities.Mimbox", null)
                        .WithMany("WaterToWashingMachineEventList")
                        .HasForeignKey("MimboxId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Mimbly.Domain.Entities.Company", b =>
                {
                    b.HasOne("Mimbly.Domain.Entities.Company", "ParentCompany")
                        .WithMany("ChildCompanyList")
                        .HasForeignKey("ParentId");

                    b.Navigation("ParentCompany");
                });

            modelBuilder.Entity("Mimbly.Domain.Entities.CompanyContact", b =>
                {
                    b.HasOne("Mimbly.Domain.Entities.Company", null)
                        .WithMany("ContactList")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Mimbly.Domain.Entities.Mimbox", b =>
                {
                    b.HasOne("Mimbly.Domain.Entities.Company", "Company")
                        .WithMany("MimboxList")
                        .HasForeignKey("CompanyId");

                    b.HasOne("Mimbly.Domain.Entities.MimboxLocation", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");

                    b.HasOne("Mimbly.Domain.Entities.MimboxModel", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mimbly.Domain.Entities.MimboxStatus", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Location");

                    b.Navigation("Model");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Mimbly.Domain.Entities.MimboxContact", b =>
                {
                    b.HasOne("Mimbly.Domain.Entities.Mimbox", null)
                        .WithMany("ContactList")
                        .HasForeignKey("MimboxId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Mimbly.Domain.Entities.MimboxLog", b =>
                {
                    b.HasOne("Mimbly.Domain.Entities.Mimbox", null)
                        .WithMany("LogList")
                        .HasForeignKey("MimboxId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Mimbly.Domain.Entities.Company", b =>
                {
                    b.Navigation("ChildCompanyList");

                    b.Navigation("ContactList");

                    b.Navigation("MimboxList");
                });

            modelBuilder.Entity("Mimbly.Domain.Entities.Mimbox", b =>
                {
                    b.Navigation("ContactList");

                    b.Navigation("ErrorLogList");

                    b.Navigation("EventLogList");

                    b.Navigation("LogList");

                    b.Navigation("WaterToWashingMachineEventList");
                });
#pragma warning restore 612, 618
        }
    }
}
