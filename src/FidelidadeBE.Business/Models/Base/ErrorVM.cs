using System.ComponentModel.DataAnnotations;

namespace FidelidadeBE.Business.Models.Base;

public class ErrorVM
{
    [Required]
    public bool Success { get; set; }

    [Required]
    public IEnumerable<string> Errors { get; set; }

    public ErrorVM(IEnumerable<string> errors)
    {
        Success = false;
        Errors = errors;
    }
}