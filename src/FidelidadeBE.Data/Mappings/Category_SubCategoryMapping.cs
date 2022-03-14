using FidelidadeBE.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FidelidadeBE.Data.Mappings;

public class Category_SubCategoryMapping : IEntityTypeConfiguration<Category_SubCategory>
{
    public void Configure(EntityTypeBuilder<Category_SubCategory> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.UpdatedAt);

        builder.HasOne(p => p.ParentCategory)
            .WithMany(s => s.SubCategories!)
            .HasForeignKey(p => p.ParentCategoryId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasOne(p => p.SubCategory)
            .WithOne(s => s.DependentCategory)
            .HasPrincipalKey<Category>(s => s.Id)
            .HasForeignKey<Category_SubCategory>(p => p.SubCategoryId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.Navigation(n => n.ParentCategory)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        builder.Navigation(n => n.SubCategory)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        builder.ToTable("Category_SubCategories");
    }
}