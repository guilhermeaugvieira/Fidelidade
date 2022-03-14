using FidelidadeBE.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FidelidadeBE.Data.Mappings;

public class CategoryMapping : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.UpdatedAt);

        builder.Property(p => p.Name)
            .HasColumnType("VARCHAR(30)")
            .IsRequired();

        builder.Property(p => p.Level)
            .IsRequired();

        builder.Navigation(n => n.Products)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        builder.Navigation(n => n.SubCategories)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        builder.Navigation(n => n.DependentCategory)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        builder.ToTable("Categories");
    }
}