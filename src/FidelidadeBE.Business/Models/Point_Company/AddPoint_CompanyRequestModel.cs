using System.ComponentModel.DataAnnotations;

namespace FidelidadeBE.Business.Models.Point_Company;

public class AddPoint_CompanyRequestModel
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "{0} must be an integer greater than {1} lesser than{2}")]
    public int Points { get; set; }
}