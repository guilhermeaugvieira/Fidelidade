using FidelidadeBE.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FidelidadeBE.Data.Mappings;

public class ClientMapping : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.UpdatedAt);

        builder.Property(p => p.CPF)
            .HasColumnType("VARCHAR(11)")
            .IsRequired();

        builder.HasOne(p => p.Address)
            .WithOne(s => s.Client)
            .HasForeignKey<Client>(p => p.AddressId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasOne(p => p.User)
            .WithOne(s => s.Client)
            .HasForeignKey<Client>(p => p.UserId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasIndex(p => p.CPF)
            .IsUnique();

        builder.Navigation(n => n.Address)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        builder.Navigation(n => n.User)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        builder.Navigation(n => n.Points)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        builder.ToTable("Clients");
    }
}