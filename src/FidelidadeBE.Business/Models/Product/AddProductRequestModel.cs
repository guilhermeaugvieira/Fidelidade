using System.ComponentModel.DataAnnotations;

namespace FidelidadeBE.Business.Models.Product;

public class AddProductRequestModel
{
    [Required]
    [StringLength(30, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 3)]
    public string Name { get; set; } = null!;

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "{0} must be at least {1}")]
    public int Points { get; set; }

    [Required]
    [MinLength(3, ErrorMessage = "{0} must have at least {1} characters")]
    public string CategoryPath { get; set; } = null!;
}