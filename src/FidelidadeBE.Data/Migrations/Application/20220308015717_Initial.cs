using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FidelidadeBE.Data.Migrations.Application;

public partial class Initial : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterDatabase()
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
                "Addresses",
                table => new
                {
                    Id = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci"),
                    State = table.Column<string>("VARCHAR(2)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    City = table.Column<string>("VARCHAR(30)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    District = table.Column<string>("VARCHAR(30)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CEP = table.Column<string>("VARCHAR(9)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Street = table.Column<string>("VARCHAR(30)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Number = table.Column<int>("int", nullable: true),
                    CreatedAt = table.Column<DateTime>("datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>("datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
                "Categories",
                table => new
                {
                    Id = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>("VARCHAR(30)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Level = table.Column<int>("int", nullable: false),
                    CreatedAt = table.Column<DateTime>("datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>("datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
                "Users",
                table => new
                {
                    Id = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>("VARCHAR(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdentityId = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>("datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>("datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
                "Category_SubCategories",
                table => new
                {
                    Id = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci"),
                    ParentCategoryId = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci"),
                    SubCategoryId = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>("datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>("datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category_SubCategories", x => x.Id);
                    table.ForeignKey(
                        "FK_Category_SubCategories_Categories_ParentCategoryId",
                        x => x.ParentCategoryId,
                        "Categories",
                        "Id");
                    table.ForeignKey(
                        "FK_Category_SubCategories_Categories_SubCategoryId",
                        x => x.SubCategoryId,
                        "Categories",
                        "Id");
                })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
                "Products",
                table => new
                {
                    Id = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>("VARCHAR(30)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Points = table.Column<int>("int", nullable: false),
                    CategoryId = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>("datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>("datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        "FK_Products_Categories_CategoryId",
                        x => x.CategoryId,
                        "Categories",
                        "Id");
                })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
                "Clients",
                table => new
                {
                    Id = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci"),
                    AddressId = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci"),
                    CPF = table.Column<string>("VARCHAR(11)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>("datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>("datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        "FK_Clients_Addresses_AddressId",
                        x => x.AddressId,
                        "Addresses",
                        "Id");
                    table.ForeignKey(
                        "FK_Clients_Users_UserId",
                        x => x.UserId,
                        "Users",
                        "Id");
                })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
                "Companies",
                table => new
                {
                    Id = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci"),
                    AddressId = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci"),
                    CNPJ = table.Column<string>("VARCHAR(14)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>("datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>("datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        "FK_Companies_Addresses_AddressId",
                        x => x.AddressId,
                        "Addresses",
                        "Id");
                    table.ForeignKey(
                        "FK_Companies_Users_UserId",
                        x => x.UserId,
                        "Users",
                        "Id");
                })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
                "Points",
                table => new
                {
                    Id = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci"),
                    ClientId = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci"),
                    AssignedPoints = table.Column<int>("int", nullable: false),
                    CreatedAt = table.Column<DateTime>("datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>("datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Points", x => x.Id);
                    table.ForeignKey(
                        "FK_Points_Clients_ClientId",
                        x => x.ClientId,
                        "Clients",
                        "Id");
                })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
                "Point_Company",
                table => new
                {
                    Id = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci"),
                    PointId = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci"),
                    CompanyId = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>("datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>("datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Point_Company", x => x.Id);
                    table.ForeignKey(
                        "FK_Point_Company_Companies_CompanyId",
                        x => x.CompanyId,
                        "Companies",
                        "Id");
                    table.ForeignKey(
                        "FK_Point_Company_Points_PointId",
                        x => x.PointId,
                        "Points",
                        "Id");
                })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
                "Point_Product",
                table => new
                {
                    Id = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci"),
                    ProductId = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci"),
                    PointId = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>("datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>("datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Point_Product", x => x.Id);
                    table.ForeignKey(
                        "FK_Point_Product_Points_PointId",
                        x => x.PointId,
                        "Points",
                        "Id");
                    table.ForeignKey(
                        "FK_Point_Product_Products_ProductId",
                        x => x.ProductId,
                        "Products",
                        "Id");
                })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
                "OrderDetails",
                table => new
                {
                    Id = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci"),
                    Point_ProductId = table.Column<Guid>("char(36)", nullable: false, collation: "ascii_general_ci"),
                    DeliveryStatus = table.Column<string>("VARCHAR(30)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>("datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>("datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        "FK_OrderDetails_Point_Product_Point_ProductId",
                        x => x.Point_ProductId,
                        "Point_Product",
                        "Id");
                })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateIndex(
            "IX_Category_SubCategories_ParentCategoryId",
            "Category_SubCategories",
            "ParentCategoryId");

        migrationBuilder.CreateIndex(
            "IX_Category_SubCategories_SubCategoryId",
            "Category_SubCategories",
            "SubCategoryId",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_Clients_AddressId",
            "Clients",
            "AddressId",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_Clients_CPF",
            "Clients",
            "CPF",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_Clients_UserId",
            "Clients",
            "UserId",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_Companies_AddressId",
            "Companies",
            "AddressId",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_Companies_CNPJ",
            "Companies",
            "CNPJ",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_Companies_UserId",
            "Companies",
            "UserId",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_OrderDetails_Point_ProductId",
            "OrderDetails",
            "Point_ProductId",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_Point_Company_CompanyId",
            "Point_Company",
            "CompanyId");

        migrationBuilder.CreateIndex(
            "IX_Point_Company_PointId",
            "Point_Company",
            "PointId",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_Point_Product_PointId",
            "Point_Product",
            "PointId",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_Point_Product_ProductId",
            "Point_Product",
            "ProductId",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_Points_ClientId",
            "Points",
            "ClientId");

        migrationBuilder.CreateIndex(
            "IX_Products_CategoryId",
            "Products",
            "CategoryId");

        migrationBuilder.CreateIndex(
            "IX_Users_IdentityId",
            "Users",
            "IdentityId",
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "Category_SubCategories");

        migrationBuilder.DropTable(
            "OrderDetails");

        migrationBuilder.DropTable(
            "Point_Company");

        migrationBuilder.DropTable(
            "Point_Product");

        migrationBuilder.DropTable(
            "Companies");

        migrationBuilder.DropTable(
            "Points");

        migrationBuilder.DropTable(
            "Products");

        migrationBuilder.DropTable(
            "Clients");

        migrationBuilder.DropTable(
            "Categories");

        migrationBuilder.DropTable(
            "Addresses");

        migrationBuilder.DropTable(
            "Users");
    }
}