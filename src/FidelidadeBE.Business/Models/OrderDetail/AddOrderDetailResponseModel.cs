namespace FidelidadeBE.Business.Models.OrderDetail;

public class AddOrderDetailResponseModel
{
    public Guid Id { get; set; }
    public string DeliveryStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public OrderDetailAdd_ProductResponseModel Product { get; set; }
}