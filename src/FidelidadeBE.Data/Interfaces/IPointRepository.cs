using System.Linq.Expressions;
using FidelidadeBE.Business.Entities;

namespace FidelidadeBE.Data.Interfaces;

public interface IPointRepository
{
    Task<IEnumerable<Point>> GetManyAsync(Expression<Func<Point, bool>> filter, bool isTrackingDisabled = false);

    Task<IEnumerable<Point>> GetPointsReport(Expression<Func<Point, bool>> filter, bool isTrackingDisabled = false);
}