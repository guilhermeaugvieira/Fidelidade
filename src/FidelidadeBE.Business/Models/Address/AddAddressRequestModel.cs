using System.ComponentModel.DataAnnotations;

namespace FidelidadeBE.Business.Models.Address;

public class AddAddressRequestModel
{
    [Required]
    [StringLength(2, ErrorMessage = "{0} must contain just {2} characters", MinimumLength = 2)]
    public string State { get; set; } = null!;

    [Required]
    [StringLength(30, ErrorMessage = "{0} must contain between {2} and {1} characters", MinimumLength = 3)]
    public string City { get; set; } = null!;

    [Required]
    [StringLength(30, ErrorMessage = "{0} must contain between {2} and {1} characters", MinimumLength = 3)]
    public string District { get; set; } = null!;

    [Required]
    [StringLength(9, ErrorMessage = "{0} must contain just {2} characters", MinimumLength = 9)]
    public string CEP { get; set; } = null!;

    [Required]
    [StringLength(30, ErrorMessage = "{0} must contain between {2} and {1} characters", MinimumLength = 3)]
    public string Street { get; set; } = null!;

    [Range(1, int.MaxValue, ErrorMessage = "{0} must be at least {1}")]
    public int? Number { get; set; }
}