using FidelidadeBE.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FidelidadeBE.Data.Mappings;

public class CompanyMapping : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.UpdatedAt);

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
    }
}