﻿// <auto-generated />
using System;
using FidelidadeBE.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FidelidadeBE.Data.Migrations.Application
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220308015717_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FidelidadeBE.Business.Entities.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasColumnType("VARCHAR(9)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("VARCHAR(30)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("VARCHAR(30)");

                    b.Property<int?>("Number")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("VARCHAR(2)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("VARCHAR(30)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Addresses", (string)null);
                });

            modelBuilder.Entity("FidelidadeBE.Business.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(30)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Categories", (string)null);
                });

            modelBuilder.Entity("FidelidadeBE.Business.Entities.Category_SubCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("ParentCategoryId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("SubCategoryId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.HasIndex("SubCategoryId")
                        .IsUnique();

                    b.ToTable("Category_SubCategories", (string)null);
                });

            modelBuilder.Entity("FidelidadeBE.Business.Entities.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("char(36)");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("VARCHAR(11)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.HasIndex("CPF")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Clients", (string)null);
                });

            modelBuilder.Entity("FidelidadeBE.Business.Entities.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("char(36)");

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasColumnType("VARCHAR(14)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.HasIndex("CNPJ")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Companies", (string)null);
                });

            modelBuilder.Entity("FidelidadeBE.Business.Entities.OrderDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DeliveryStatus")
                        .IsRequired()
                        .HasColumnType("VARCHAR(30)");

                    b.Property<Guid>("Point_ProductId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("Point_ProductId")
                        .IsUnique();

                    b.ToTable("OrderDetails", (string)null);
                });

            modelBuilder.Entity("FidelidadeBE.Business.Entities.Point", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("AssignedPoints")
                        .HasColumnType("int");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Points", (string)null);
                });

            modelBuilder.Entity("FidelidadeBE.Business.Entities.Point_Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("PointId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("PointId")
                        .IsUnique();

                    b.ToTable("Point_Company", (string)null);
                });

            modelBuilder.Entity("FidelidadeBE.Business.Entities.Point_Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("PointId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("PointId")
                        .IsUnique();

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("Point_Product", (string)null);
                });

            modelBuilder.Entity("FidelidadeBE.Business.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(30)");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("FidelidadeBE.Business.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("IdentityId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("IdentityId")
                        .IsUnique();

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("FidelidadeBE.Business.Entities.Category_SubCategory", b =>
                {
                    b.HasOne("FidelidadeBE.Business.Entities.Category", "ParentCategory")
                        .WithMany("SubCategories")
                        .HasForeignKey("ParentCategoryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("FidelidadeBE.Business.Entities.Category", "SubCategory")
                        .WithOne("DependentCategory")
                        .HasForeignKey("FidelidadeBE.Business.Entities.Category_SubCategory", "SubCategoryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ParentCategory");

                    b.Navigation("SubCategory");
                });

            modelBuilder.Entity("FidelidadeBE.Business.Entities.Client", b =>
                {
                    b.HasOne("FidelidadeBE.Business.Entities.Address", "Address")
                        .WithOne("Client")
                        .HasForeignKey("FidelidadeBE.Business.Entities.Client", "AddressId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("FidelidadeBE.Business.Entities.User", "User")
                        .WithOne("Client")
                        .HasForeignKey("FidelidadeBE.Business.Entities.Client", "UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FidelidadeBE.Business.Entities.Company", b =>
                {
                    b.HasOne("FidelidadeBE.Business.Entities.Address", "Address")
                        .WithOne("Company")
                        .HasForeignKey("FidelidadeBE.Business.Entities.Company", "AddressId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("FidelidadeBE.Business.Entities.User", "User")
                        .WithOne("Company")
                        .HasForeignKey("FidelidadeBE.Business.Entities.Company", "UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FidelidadeBE.Business.Entities.OrderDetail", b =>
                {
                    b.HasOne("FidelidadeBE.Business.Entities.Point_Product", "Product")
                        .WithOne("OrderDetail")
                        .HasForeignKey("FidelidadeBE.Business.Entities.OrderDetail", "Point_ProductId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("FidelidadeBE.Business.Entities.Point", b =>
                {
                    b.HasOne("FidelidadeBE.Business.Entities.Client", "Client")
                        .WithMany("Points")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("FidelidadeBE.Business.Entities.Point_Company", b =>
                {
                    b.HasOne("FidelidadeBE.Business.Entities.Company", "Company")
                        .WithMany("Points")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("FidelidadeBE.Business.Entities.Point", "Point")
                        .WithOne("Company")
                        .HasForeignKey("FidelidadeBE.Business.Entities.Point_Company", "PointId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Point");
                });

            modelBuilder.Entity("FidelidadeBE.Business.Entities.Point_Product", b =>
                {
                    b.HasOne("FidelidadeBE.Business.Entities.Point", "Point")
                        .WithOne("Product")
                        .HasForeignKey("FidelidadeBE.Business.Entities.Point_Product", "PointId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("FidelidadeBE.Business.Entities.Product", "Product")
                        .WithOne("Point")
                        .HasForeignKey("FidelidadeBE.Business.Entities.Point_Product", "ProductId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Point");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("FidelidadeBE.Business.Entities.Product", b =>
                {
                    b.HasOne("FidelidadeBE.Business.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("FidelidadeBE.Business.Entities.Address", b =>
                {
                    b.Navigation("Client");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("FidelidadeBE.Business.Entities.Category", b =>
                {
                    b.Navigation("DependentCategory");

                    b.Navigation("Products");

                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("FidelidadeBE.Business.Entities.Client", b =>
                {
                    b.Navigation("Points");
                });

            modelBuilder.Entity("FidelidadeBE.Business.Entities.Company", b =>
                {
                    b.Navigation("Points");
                });

            modelBuilder.Entity("FidelidadeBE.Business.Entities.Point", b =>
                {
                    b.Navigation("Company");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("FidelidadeBE.Business.Entities.Point_Product", b =>
                {
                    b.Navigation("OrderDetail");
                });

            modelBuilder.Entity("FidelidadeBE.Business.Entities.Product", b =>
                {
                    b.Navigation("Point");
                });

            modelBuilder.Entity("FidelidadeBE.Business.Entities.User", b =>
                {
                    b.Navigation("Client");

                    b.Navigation("Company");
                });
#pragma warning restore 612, 618
        }
    }
}
