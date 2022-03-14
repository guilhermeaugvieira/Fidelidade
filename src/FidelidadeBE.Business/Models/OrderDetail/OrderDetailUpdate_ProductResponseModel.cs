namespace FidelidadeBE.Business.Models.OrderDetail;

public class OrderDetailUpdate_ProductResponseModel
{
    public Guid Id { get; set; }
    public string Name { get; private set; }
    public int Points { get; private set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}