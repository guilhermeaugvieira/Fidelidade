using FidelidadeBE.Business.Models.OrderDetail;
using FidelidadeBE.Business.Models.Point;
using FidelidadeBE.Business.Models.Point_Company;

namespace FidelidadeBE.Application.Interfaces;

public interface IPointApplicationService
{
    Task<AddPoint_CompanyResponseModel?> AssignPointsToClient(string clientCpf,
        AddPoint_CompanyRequestModel pointCompanyInfo);

    Task<AddOrderDetailResponseModel?> RedeemProduct(Guid productId);

    Task<PointReportResponseModel?> GeneratePointReport();
}