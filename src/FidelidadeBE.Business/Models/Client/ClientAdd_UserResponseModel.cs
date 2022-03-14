using System.ComponentModel.DataAnnotations;

namespace FidelidadeBE.Business.Models.Client;

public class ClientAdd_UserResponseModel
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public Guid IdentityId { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime? UpdatedAt { get; set; }
}