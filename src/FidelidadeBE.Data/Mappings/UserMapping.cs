using FidelidadeBE.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FidelidadeBE.Data.Mappings;

public class UserMapping : BaseEntityMapping<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(p => p.Name)
            .HasColumnType("VARCHAR(50)")
            .IsRequired();

        builder.Property(p => p.IdentityId)
            .IsRequired();

        builder.HasIndex(p => p.IdentityId)
            .IsUnique();

        builder.Navigation(n => n.Client)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        builder.Navigation(n => n.Company)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        builder.ToTable("Users");
        
        base.Configure(builder);
    }
}