﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PM_Case_Managemnt_API.Data;

#nullable disable

namespace PMCaseManagemntAPI.Migrations.DB
{
    [DbContext(typeof(DBContext))]
    [Migration("20230116081829_addIsHeadOffice")]
    partial class addIsHeadOffice
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PM_Case_Managemnt_API.Models.Common.Employee.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remark")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RowStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("PM_Case_Managemnt_API.Models.Common.Employee.EmployeeStructures", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrganizationalStructureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Remark")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RowStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("OrganizationalStructureId");

                    b.ToTable("EmployeesStructures");
                });

            modelBuilder.Entity("PM_Case_Managemnt_API.Models.Common.Organization.OrganizationBranch", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsHeadOffice")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrganizationProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remark")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RowStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationProfileId");

                    b.ToTable("OrganizationBranches");
                });

            modelBuilder.Entity("PM_Case_Managemnt_API.Models.Common.Organization.OrganizationProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrganizationNameEnglish")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrganizationNameInLocalLanguage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remark")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RowStatus")
                        .HasColumnType("int");

                    b.Property<int>("SmsCode")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OrganizationProfile");
                });

            modelBuilder.Entity("PM_Case_Managemnt_API.Models.Common.Organization.OrganizationalStructure", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<Guid>("OrganizationBranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ParentStructureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Remark")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RowStatus")
                        .HasColumnType("int");

                    b.Property<string>("StructureName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationBranchId");

                    b.HasIndex("ParentStructureId");

                    b.ToTable("OrganizationalStructures");
                });

            modelBuilder.Entity("PM_Case_Managemnt_API.Models.Common.Employee.EmployeeStructures", b =>
                {
                    b.HasOne("PM_Case_Managemnt_API.Models.Common.Employee.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PM_Case_Managemnt_API.Models.Common.Organization.OrganizationalStructure", "OrganizationalStructure")
                        .WithMany()
                        .HasForeignKey("OrganizationalStructureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("OrganizationalStructure");
                });

            modelBuilder.Entity("PM_Case_Managemnt_API.Models.Common.Organization.OrganizationBranch", b =>
                {
                    b.HasOne("PM_Case_Managemnt_API.Models.Common.Organization.OrganizationProfile", "OrganizationProfile")
                        .WithMany()
                        .HasForeignKey("OrganizationProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrganizationProfile");
                });

            modelBuilder.Entity("PM_Case_Managemnt_API.Models.Common.Organization.OrganizationalStructure", b =>
                {
                    b.HasOne("PM_Case_Managemnt_API.Models.Common.Organization.OrganizationBranch", "OrganizationBranch")
                        .WithMany()
                        .HasForeignKey("OrganizationBranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PM_Case_Managemnt_API.Models.Common.Organization.OrganizationalStructure", "ParentStructure")
                        .WithMany()
                        .HasForeignKey("ParentStructureId");

                    b.Navigation("OrganizationBranch");

                    b.Navigation("ParentStructure");
                });
#pragma warning restore 612, 618
        }
    }
}
