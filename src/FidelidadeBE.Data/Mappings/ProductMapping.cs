using FidelidadeBE.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FidelidadeBE.Data.Mappings;

public class ProductMapping : BaseEntityMapping<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Name)
            .HasColumnType("VARCHAR(30)")
            .IsRequired();

        builder.Property(p => p.Points)
            .IsRequired();

        builder.HasOne(p => p.Category)
            .WithMany(s => s.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.Navigation(p => p.Point)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        builder.Navigation(p => p.Category)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        builder.ToTable("Products");
        
        base.Configure(builder);
    }
}