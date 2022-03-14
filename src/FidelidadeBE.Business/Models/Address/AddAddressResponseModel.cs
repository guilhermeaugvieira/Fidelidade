using System.ComponentModel.DataAnnotations;

namespace FidelidadeBE.Business.Models.Address;

public class AddAddressResponseModel
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string State { get; set; } = null!;

    [Required]
    public string City { get; set; } = null!;

    [Required]
    public string District { get; set; } = null!;

    [Required]
    public string CEP { get; set; } = null!;

    [Required]
    public string Street { get; set; } = null!;

    public int? Number { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime? UpdatedAt { get; set; }
}