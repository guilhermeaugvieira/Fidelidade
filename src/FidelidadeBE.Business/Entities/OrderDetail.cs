using FidelidadeBE.Business.Types.OrderDetail;

namespace FidelidadeBE.Business.Entities;

public class OrderDetail : BaseEntity
{
    public Guid Point_ProductId { get; private set; }
    public string DeliveryStatus { get; private set; }
    public virtual Point_Product? Product { get; private set; }

    protected OrderDetail() {}

    public OrderDetail(Point_Product product, string deliveryStatus)
    {
        Product = product;
        Point_ProductId = product.Id;
        DeliveryStatus = deliveryStatus;
    }

    public void UpdateDeliveryStatus(DeliveryStatusType status)
    {
        DeliveryStatus = status.ToString().Replace('_', ' ');

        UpdateChangeDate();
    }
}