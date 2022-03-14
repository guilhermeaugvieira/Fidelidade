using AutoMapper;
using FidelidadeBE.Application.Interfaces;
using FidelidadeBE.Business.Entities;
using FidelidadeBE.Business.Entities.Validations;
using FidelidadeBE.Business.Interfaces;
using FidelidadeBE.Business.Models.Company;
using FidelidadeBE.Core.Interfaces;
using FidelidadeBE.Core.Notifications;
using FidelidadeBE.Data.Interfaces;
using FidelidadeBE.Infra.Interfaces;

namespace FidelidadeBE.Application.Services;

public class CompanyApplicationService : ICompanyApplicationService
{
    private readonly INotificator _notificator;
    private readonly IIdentityApplicationService _identityApplicationService;
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;
    private readonly IDomainBaseService _domainBaseService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;

    public CompanyApplicationService(INotificator notificator,
        IIdentityApplicationService identityApplicationService,
        ICompanyRepository companyRepository,
        IMapper mapper,
        IDomainBaseService domainBaseService,
        IUnitOfWork unitOfWork,
        IJwtService jwtService
    )
    {
        _notificator = notificator;
        _identityApplicationService = identityApplicationService;
        _companyRepository = companyRepository;
        _mapper = mapper;
        _domainBaseService = domainBaseService;
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
    }

    public async Task<AddCompanyResponseModel?> AddCompanyAsync(AddCompanyRequestModel companyInfo)
    {
        if (await _identityApplicationService.GetUserByEmailAsync(companyInfo.User.Email) != null)
        {
            _notificator.AddNotification("Email is already registered", NotificationType.BusinessRules);
            return null;
        }

        if (await _companyRepository.GetAsync(x => x.CNPJ == companyInfo.CNPJ, true) != null)
        {
            _notificator.AddNotification("The CNPJ is being used by another user", NotificationType.BusinessRules);
            return null;
        }

        var identityUser =
            _identityApplicationService.GenerateIdentityUser(companyInfo.User.Email, true);

        await _identityApplicationService.BeginTransactionAsync();

        if (!await _identityApplicationService.CreateUserAsync(identityUser, companyInfo.User.Password))
            return null;

        await _identityApplicationService.CreateRoleAsync("Company");

        await _identityApplicationService.AddRoleToUserAsync(identityUser, "Company");

        var company = await CreateCompanyAsync(companyInfo);

        if (!_domainBaseService.IsEntityValid(new CompanyValidation(), company))
        {
            await _identityApplicationService.RollbackChangesAsync();

            return null;
        }

        await _companyRepository.AddAsync(company);

        if (!await _unitOfWork.CommitAsync())
        {
            _notificator.AddNotification("Any changes were done at the database", NotificationType.IncorrectData);

            await _identityApplicationService.RollbackChangesAsync();

            return null;
        }

        await _identityApplicationService.CommitChangesAsync();

        var companyResponse = _mapper.Map<AddCompanyResponseModel>(company);

        companyResponse.AccessToken = await _jwtService.GenerateJwt(companyInfo.User.Email);

        return companyResponse;
    }

    private async Task<Company> CreateCompanyAsync(AddCompanyRequestModel companyInfo)
    {
        var company = _mapper.Map<Company>(companyInfo);

        company.User!.SetIdentityId(
            Guid.Parse((await _identityApplicationService.GetUserByEmailAsync(companyInfo.User.Email))!.Id)
        );

        return company;
    }
}