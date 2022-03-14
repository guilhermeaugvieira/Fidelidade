using FidelidadeBE.Business.Models.Address;
using System.ComponentModel.DataAnnotations;

namespace FidelidadeBE.Business.Models.Client;

public class AddClientResponseModel
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string CPF { get; set; } = null!;

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime? UpdatedAt { get; set; }

    [Required]
    public AddAddressResponseModel Address { get; set; } = null!;

    [Required]
    public ClientAdd_UserResponseModel User { get; set; } = null!;

    [Required]
    public string AccessToken { get; set; } = null!;
}