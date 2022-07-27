using FidelidadeBE.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FidelidadeBE.Data.Mappings;

public class CategoryMapping : BaseEntityMapping<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
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
        
        base.Configure(builder);
    }
}