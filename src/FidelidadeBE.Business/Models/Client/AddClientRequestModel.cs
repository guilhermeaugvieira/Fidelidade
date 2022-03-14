using FidelidadeBE.Business.Models.Address;
using FidelidadeBE.Business.Models.User;
using System.ComponentModel.DataAnnotations;

namespace FidelidadeBE.Business.Models.Client;

public class AddClientRequestModel
{
    [Required]
    [StringLength(11, ErrorMessage = "{0} must contain {2} characters", MinimumLength = 11)]
    public string CPF { get; set; } = null!;

    [Required]
    public AddAddressRequestModel Address { get; set; } = null!;

    [Required]
    public AddUserRequestModel User { get; set; } = null!;
}