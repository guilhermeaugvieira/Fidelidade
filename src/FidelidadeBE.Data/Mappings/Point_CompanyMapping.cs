using FidelidadeBE.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FidelidadeBE.Data.Mappings;

public class Point_CompanyMapping : BaseEntityMapping<Point_Company>
{
    public override void Configure(EntityTypeBuilder<Point_Company> builder)
    {
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
        
        base.Configure(builder);
    }
}