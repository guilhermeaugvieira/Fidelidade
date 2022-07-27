using FidelidadeBE.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FidelidadeBE.Data.Mappings;

public class PointMapping : BaseEntityMapping<Point>
{
    public override void Configure(EntityTypeBuilder<Point> builder)
    {
        builder.Property(p => p.AssignedPoints)
            .IsRequired();

        builder.HasOne(p => p.Client)
            .WithMany(s => s.Points)
            .HasForeignKey(p => p.ClientId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.Navigation(n => n.Client)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        builder.Navigation(n => n.Company)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        builder.Navigation(n => n.Product)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        builder.ToTable("Points");

        base.Configure(builder);
    }
}