using System.ComponentModel.DataAnnotations;

namespace FidelidadeBE.Business.Models.User;

public class AddUserResponseModel
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public Guid IdentityId { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime? UpdatedAt { get; set; }

    [Required]
    public string AccessToken { get; set; } = null!;
}