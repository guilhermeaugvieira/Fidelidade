using FidelidadeBE.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FidelidadeBE.Data.Mappings;

public class ProductMapping : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.UpdatedAt);

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
    }
}