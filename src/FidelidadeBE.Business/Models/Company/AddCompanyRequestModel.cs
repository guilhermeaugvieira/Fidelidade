using System.ComponentModel.DataAnnotations;
using FidelidadeBE.Business.Models.Address;
using FidelidadeBE.Business.Models.User;

namespace FidelidadeBE.Business.Models.Company;

public class AddCompanyRequestModel
{
    [Required]
    [StringLength(14, ErrorMessage = "{0} must contain {2} characters", MinimumLength = 14)]
    public string CNPJ { get; set; } = null!;

    [Required]
    public AddAddressRequestModel Address { get; set; } = null!;

    [Required]
    public AddUserRequestModel User { get; set; } = null!;
}