namespace FidelidadeBE.Infra.Interfaces;

public interface IJwtService
{
    Task<string> GenerateJwt(string email);
}