using System.ComponentModel.DataAnnotations;

namespace FidelidadeBE.Business.Models.User;

public class AddUserRequestModel
{
    [Required]
    [StringLength(50, ErrorMessage = "{0} must contain between {2} and {1} characters", MinimumLength = 3)]
    public string Name { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}