using FidelidadeBE.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FidelidadeBE.Data.Mappings;

public class Point_CompanyMapping : IEntityTypeConfiguration<Point_Company>
{
    public void Configure(EntityTypeBuilder<Point_Company> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.UpdatedAt);

        builder.HasOne(p => p.Company)
            .WithMany(s => s.Points)
            .HasForeignKey(p => p.CompanyId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasOne(p => p.Point)
            .WithOne(s => s.Company)
            .HasForeignKey<Point_Company>(p => p.PointId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.Navigation(n => n.Company)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        builder.Navigation(n => n.Point)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        builder.ToTable("Point_Company");
    }
}