using FidelidadeBE.Business.Models.Category;

namespace FidelidadeBE.Business.Models.Product;

public class AddProductResponseModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public int Points { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public AddCategoryResponseModel Category { get; set; } = null!;
}