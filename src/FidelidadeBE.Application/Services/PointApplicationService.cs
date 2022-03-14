using AutoMapper;
using FidelidadeBE.Application.Interfaces;
using FidelidadeBE.Business.Entities;
using FidelidadeBE.Business.Entities.Validations;
using FidelidadeBE.Business.Interfaces;
using FidelidadeBE.Business.Models.OrderDetail;
using FidelidadeBE.Business.Models.Point;
using FidelidadeBE.Business.Models.Point_Company;
using FidelidadeBE.Business.Types.OrderDetail;
using FidelidadeBE.Core.Interfaces;
using FidelidadeBE.Core.Notifications;
using FidelidadeBE.Data.Interfaces;

namespace FidelidadeBE.Application.Services;

public class PointApplicationService : IPointApplicationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly INotificator _notificator;
    private readonly IDomainBaseService _domainBaseService;
    private readonly IPoint_CompanyRepository _pointCompanyRepository;
    private readonly IPointRepository _pointRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IProductRepository _productRepository;
    private readonly IIdentityApplicationService _identityApplicationService;
    private readonly IOrderDetailRepository _orderDetailRepository;
    private readonly IMapper _mapper;

    public PointApplicationService(IUnitOfWork unitOfWork,
        INotificator notificator,
        IDomainBaseService domainBaseService,
        IPoint_CompanyRepository pointCompanyRepository,
        IPointRepository pointRepository,
        IClientRepository clientRepository,
        IProductRepository productRepository,
        IIdentityApplicationService identityApplicationService,
        IOrderDetailRepository orderDetailRepository,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _notificator = notificator;
        _domainBaseService = domainBaseService;
        _pointCompanyRepository = pointCompanyRepository;
        _pointRepository = pointRepository;
        _clientRepository = clientRepository;
        _productRepository = productRepository;
        _orderDetailRepository = orderDetailRepository;
        _identityApplicationService = identityApplicationService;
        _mapper = mapper;
    }

    public async Task<AddPoint_CompanyResponseModel?> AssignPointsToClient(string clientCpf,
        AddPoint_CompanyRequestModel pointCompanyInfo)
    {
        var client = await _clientRepository.GetAsync(x => x.CPF == clientCpf);

        if (client == null)
        {
            _notificator.AddNotification("Client doesn't exist", NotificationType.NotFoundResource);
            return null;
        }

        var company = await _identityApplicationService.GetCompanyLoggedInAsync();

        if (company == null)
        {
            _notificator.AddNotification("Company is not logged in", NotificationType.BusinessRules);
            return null;
        }

        var point = new Point(client, pointCompanyInfo.Points);

        if (!_domainBaseService.IsEntityValid(new PointValidation(), point))
            return null;

        var pointCompany = new Point_Company(point, company);

        if (!_domainBaseService.IsEntityValid(new Point_CompanyValidation(), pointCompany))
            return null;

        await _pointCompanyRepository.AddAsync(pointCompany);

        if (await _unitOfWork.CommitAsync()) return _mapper.Map<AddPoint_CompanyResponseModel>(point);

        _notificator.AddNotification("Any changes were done at the database", NotificationType.IncorrectData);
        return null;
    }

    public async Task<AddOrderDetailResponseModel?> RedeemProduct(Guid productId)
    {
        var product = await _productRepository.GetAsync(x => x.Id == productId);

        if (product == null)
        {
            _notificator.AddNotification("Any product was found", NotificationType.NotFoundResource);
            return null;
        }

        if (product.Point != null)
        {
            _notificator.AddNotification("This product already was redeemed", NotificationType.BusinessRules);
            return null;
        }

        var client = await _identityApplicationService.GetClientLoggedInAsync();

        if (client == null)
        {
            _notificator.AddNotification("There isn't client logged in", NotificationType.BusinessRules);
            return null;
        }

        var userPoints = (await _pointRepository.GetManyAsync(x => x.ClientId == client.Id)).Sum(x => x.AssignedPoints);

        if (userPoints < product.Points)
        {
            _notificator.AddNotification("There isn't enough points to redeem the product",
                NotificationType.BusinessRules);
            return null;
        }

        var point = new Point(client, product.Points * -1);
        var pointProduct = new Point_Product(product, point);
        var orderDetail = new OrderDetail(pointProduct, DeliveryStatusType.Reservado.ToString());

        if (!_domainBaseService.IsEntityValid(new OrderDetailValidation(), orderDetail))
            return null;

        await _orderDetailRepository.AddAsync(orderDetail);

        if (await _unitOfWork.CommitAsync()) return _mapper.Map<AddOrderDetailResponseModel>(orderDetail);

        _notificator.AddNotification("Any changes were done at the database", NotificationType.IncorrectData);
        return null;
    }

    public async Task<PointReportResponseModel?> GeneratePointReport()
    {
        var client = await _identityApplicationService.GetClientLoggedInAsync();

        if (client == null)
        {
            _notificator.AddNotification("Client is not logged in", NotificationType.BusinessRules);
            return null;
        }

        var points = await _pointRepository.GetPointsReport(x => x.ClientId == client.Id);

        var clientPoints = points.ToList();

        if (!clientPoints.Any())
            return null;

        var reportResponse = new PointReportResponseModel
        {
            AvailablePoints = clientPoints.Sum(x => x.AssignedPoints),
            Points = new List<PointReport_PointResponseModel>()
        };

        foreach (var point in clientPoints)
            reportResponse.Points.Add(FormatPoint(point));

        return reportResponse;
    }

    private PointReport_PointResponseModel FormatPoint(Point point)
    {
        PointReport_CompanyResponseModel? company;
        PointReport_ProductResponseModel? product;

        if (point.Company == null)
            company = null;
        else
            company = new PointReport_CompanyResponseModel
            {
                Id = point.Company.Company!.Id,
                CNPJ = point.Company.Company!.CNPJ,
                Address = new PointReport_CompanyAddressResponseModel
                {
                    Id = point.Company.Company!.Address!.Id,
                    City = point.Company.Company!.Address!.City,
                    District = point.Company.Company!.Address!.District,
                    Number = point.Company.Company!.Address!.Number,
                    State = point.Company.Company!.Address!.State,
                    Street = point.Company.Company!.Address!.Street,
                    CEP = point.Company.Company!.Address!.CEP,
                    CreatedAt = point.Company.Company!.Address!.CreatedAt,
                    UpdatedAt = point.Company.Company!.Address!.UpdatedAt
                },
                User = new PointReport_CompanyUserReponseModel
                {
                    Id = point.Company.Company!.User!.Id,
                    Name = point.Company.Company!.User!.Name,
                    CreatedAt = point.Company.Company!.User!.CreatedAt,
                    UpdatedAt = point.Company.Company!.User!.UpdatedAt
                },
                CreatedAt = point.Company.Company!.CreatedAt,
                UpdatedAt = point.Company.Company!.UpdatedAt
            };

        if (point.Product == null)
            product = null;
        else
            product = new PointReport_ProductResponseModel
            {
                Id = point.Product.Product!.Id,
                Name = point.Product.Product!.Name,
                Category = new PointReport_ProductCategoryResponseModel
                {
                    Id = point.Product.Product!.Category!.Id,
                    Level = point.Product.Product!.Category!.Level,
                    Name = point.Product.Product!.Category!.Name,
                    CreatedAt = point.Product.Product!.Category!.CreatedAt,
                    UpdatedAt = point.Product.Product!.Category!.UpdatedAt
                },
                CreatedAt = point.Product.Product!.CreatedAt,
                UpdatedAt = point.Product.Product!.UpdatedAt
            };

        return new PointReport_PointResponseModel
        {
            Id = point.Id,
            AssignedPoints = point.AssignedPoints,
            Company = company,
            Product = product,
            CreatedAt = point.CreatedAt,
            UpdatedAt = point.UpdatedAt
        };
    }
}