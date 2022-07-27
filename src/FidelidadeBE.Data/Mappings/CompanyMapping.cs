using FidelidadeBE.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FidelidadeBE.Data.Mappings;

public class CompanyMapping : BaseEntityMapping<Company>
{
    public override void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.Property(p => p.CNPJ)
            .HasColumnType("VARCHAR(14)")
            .IsRequired();

        builder.HasOne(p => p.Address)
            .WithOne(s => s.Company)
            .HasForeignKey<Company>(p => p.AddressId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasOne(p => p.User)
            .WithOne(s => s.Company)
            .HasForeignKey<Company>(p => p.UserId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasIndex(p => p.CNPJ)
            .IsUnique();

        builder.Navigation(n => n.Address)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        builder.Navigation(n => n.User)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        builder.Navigation(n => n.Points)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        builder.ToTable("Companies");
        
        base.Configure(builder);
    }
}