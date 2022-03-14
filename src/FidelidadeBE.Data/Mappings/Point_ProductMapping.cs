using FidelidadeBE.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FidelidadeBE.Data.Mappings;

public class Point_ProductMapping : IEntityTypeConfiguration<Point_Product>
{
    public void Configure(EntityTypeBuilder<Point_Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.UpdatedAt);

        builder.HasOne(p => p.Product)
            .WithOne(s => s.Point)
            .HasForeignKey<Point_Product>(p => p.ProductId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasOne(p => p.Point)
            .WithOne(s => s.Product)
            .HasForeignKey<Point_Product>(p => p.PointId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.Navigation(n => n.Product)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        builder.Navigation(n => n.Point)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        builder.ToTable("Point_Product");
    }
}