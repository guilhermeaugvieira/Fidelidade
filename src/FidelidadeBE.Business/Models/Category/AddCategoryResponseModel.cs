namespace FidelidadeBE.Business.Models.Category;

public class AddCategoryResponseModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public int Level { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}