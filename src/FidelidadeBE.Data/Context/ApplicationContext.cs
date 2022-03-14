using FidelidadeBE.Business.Entities;
using FidelidadeBE.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FidelidadeBE.Data.Context;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    #region Entities

    public DbSet<Category> Categories { get; set; }
    public DbSet<Category_SubCategory> Category_SubCategories { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Point> Points { get; set; }
    public DbSet<Point_Company> Point_Company { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Point_Product> Point_Product { get; set; }
    public DbSet<User> Users { get; set; }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryMapping).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Category_SubCategoryMapping).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientMapping).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CompanyMapping).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AddressMapping).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PointMapping).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Point_CompanyMapping).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductMapping).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Point_ProductMapping).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserMapping).Assembly);
    }
}