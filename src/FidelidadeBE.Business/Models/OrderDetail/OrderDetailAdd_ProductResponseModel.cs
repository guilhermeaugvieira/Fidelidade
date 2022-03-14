namespace FidelidadeBE.Business.Models.OrderDetail;

public class OrderDetailAdd_ProductResponseModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Points { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}