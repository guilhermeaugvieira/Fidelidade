using FidelidadeBE.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FidelidadeBE.Data.Mappings;

public class AddressMapping : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.UpdatedAt);

        builder.Property(p => p.State)
            .HasColumnType("VARCHAR(2)")
            .IsRequired();

        builder.Property(p => p.City)
            .HasColumnType("VARCHAR(30)")
            .IsRequired();

        builder.Property(p => p.District)
            .HasColumnType("VARCHAR(30)")
            .IsRequired();

        builder.Property(p => p.CEP)
            .HasColumnType("VARCHAR(9)")
            .IsRequired();

        builder.Property(p => p.Street)
            .HasColumnType("VARCHAR(30)")
            .IsRequired();

        builder.Property(p => p.Number);

        builder.Navigation(n => n.Client)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        builder.Navigation(n => n.Company)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        builder.ToTable("Addresses");
    }
}