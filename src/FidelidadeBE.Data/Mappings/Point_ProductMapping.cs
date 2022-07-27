using FidelidadeBE.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FidelidadeBE.Data.Mappings;

public class Point_ProductMapping : BaseEntityMapping<Point_Product>
{
    public override void Configure(EntityTypeBuilder<Point_Product> builder)
    {
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
        
        base.Configure(builder);
    }
}