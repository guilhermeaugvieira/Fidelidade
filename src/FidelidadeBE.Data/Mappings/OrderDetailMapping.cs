using FidelidadeBE.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FidelidadeBE.Data.Mappings;

public class OrderDetailMapping : BaseEntityMapping<OrderDetail>
{
    public override void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.Property(p => p.DeliveryStatus)
            .HasColumnType("VARCHAR(30)")
            .IsRequired();

        builder.HasOne(p => p.Product)
            .WithOne(s => s.OrderDetail)
            .HasForeignKey<OrderDetail>(p => p.Point_ProductId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.ToTable("OrderDetails");
        
        base.Configure(builder);
    }
}