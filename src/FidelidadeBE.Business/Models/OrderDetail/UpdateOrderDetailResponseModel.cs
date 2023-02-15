namespace FidelidadeBE.Business.Models.OrderDetail;

public class UpdateOrderDetailResponseModel
{
    public Guid Id { get; set; }
    public string DeliveryStatus { get;  set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public OrderDetailUpdate_ProductResponseModel Product { get; set; }
}