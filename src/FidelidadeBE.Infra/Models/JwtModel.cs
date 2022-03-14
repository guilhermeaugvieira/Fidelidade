namespace FidelidadeBE.Infra.Models;

public class JwtModel
{
    public string Secret { get; set; } = null!;
    public int HoursToExpire { get; set; }
    public string Emitter { get; set; } = null!;
    public string ValidIn { get; set; } = null!;
}