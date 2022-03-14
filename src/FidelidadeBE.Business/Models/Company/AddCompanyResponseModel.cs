using System.ComponentModel.DataAnnotations;
using FidelidadeBE.Business.Models.Address;
using FidelidadeBE.Business.Models.Client;

namespace FidelidadeBE.Business.Models.Company;

public class AddCompanyResponseModel
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string CNPJ { get; set; } = null!;

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