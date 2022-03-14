using AutoMapper;
using FidelidadeBE.Application.Interfaces;
using FidelidadeBE.Business.Entities;
using FidelidadeBE.Business.Entities.Validations;
using FidelidadeBE.Business.Interfaces;
using FidelidadeBE.Business.Models.Address;
using FidelidadeBE.Core.Interfaces;
using FidelidadeBE.Core.Notifications;
using FidelidadeBE.Data.Interfaces;

namespace FidelidadeBE.Application.Services;

public class AddressApplicationService : IAddressApplicationService
{
    private readonly IMapper _mapper;
    private readonly INotificator _notificator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAddressRepository _addressRepository;
    private readonly IDomainBaseService _domainBaseService;
    private readonly IIdentityApplicationService _identityApplicationService;

    public AddressApplicationService(
        IMapper mapper,
        INotificator notificator,
        IUnitOfWork unitOfWork,
        IAddressRepository addressRepository,
        IDomainBaseService domainBaseService,
        IIdentityApplicationService identityApplicationService
    )
    {
        _mapper = mapper;
        _notificator = notificator;
        _unitOfWork = unitOfWork;
        _addressRepository = addressRepository;
        _domainBaseService = domainBaseService;
        _identityApplicationService = identityApplicationService;
    }

    public async Task<AddAddressResponseModel?> UpdateAddressAsync(AddAddressRequestModel newAddress, string role)
    {
        var address = await ObtainRoleAddressAsync(role);

        if (address == null)
        {
            _notificator.AddNotification("There isn't an address associated to the user logged in",
                NotificationType.BusinessRules);
            return null;
        }

        address.UpdateAddress(
            newAddress.State,
            newAddress.City,
            newAddress.District,
            newAddress.CEP,
            newAddress.Street,
            newAddress.Number
        );

        if (!_domainBaseService.IsEntityValid(new AddressValidation(), address)) return null;

        _addressRepository.Update(address);

        if (await _unitOfWork.CommitAsync()) return _mapper.Map<AddAddressResponseModel>(address);

        _notificator.AddNotification("Any changes were done at the database", NotificationType.IncorrectData);
        return null;
    }

    private async Task<Address?> ObtainRoleAddressAsync(string role)
    {
        var user = await _identityApplicationService.GetLoggedInUserWithAddressOfRelationsAsync();

        return role switch
        {
            "Client" => user!.Client!.Address,
            "Company" => user!.Company!.Address,
            _ => null
        };
    }
}