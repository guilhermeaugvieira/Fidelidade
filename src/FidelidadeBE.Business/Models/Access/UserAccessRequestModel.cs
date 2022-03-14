using System.ComponentModel.DataAnnotations;

namespace FidelidadeBE.Business.Models.Access;

public class UserAccessRequestModel
{
    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}