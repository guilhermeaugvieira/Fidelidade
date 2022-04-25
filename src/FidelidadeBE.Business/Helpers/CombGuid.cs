using RT.Comb;

namespace FidelidadeBE.Business.Helpers;

public static class CombGuid
{
    public static Guid Generate()
    {
        return new SqlCombProvider(new UnixDateTimeStrategy()).Create();
    }
}