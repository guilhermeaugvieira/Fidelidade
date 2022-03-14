using AutoMapper;
using FidelidadeBE.Application.Interfaces;
using FidelidadeBE.Business.Entities;
using FidelidadeBE.Business.Models.Client;
using FidelidadeBE.Core.Interfaces;
using FidelidadeBE.Core.Notifications;
using FidelidadeBE.Data.Interfaces;
using FidelidadeBE.Infra.Interfaces;
using FidelidadeBE.Business.Entities.Validations;
using FidelidadeBE.Business.Interfaces;

namespace FidelidadeBE.Application.Services;

public class ClientApplicationService : IClientApplicationService
{
    private readonly IMapper _mapper;
    private readonly INotificator _notificator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClientRepository _clientRepository;
    private readonly IJwtService _jwtService;
    private readonly IDomainBaseService _domainBaseService;
    private readonly IIdentityApplicationService _identityApplicationService;

    public ClientApplicationService(
        IMapper mapper,
        INotificator notificator,
        IUnitOfWork unitOfWork,
        IClientRepository clientRepository,
        IJwtService jwtService,
        IDomainBaseService domainBaseService,
        IIdentityApplicationService identityApplicationService
    )
    {
        _mapper = mapper;
        _notificator = notificator;
        _unitOfWork = unitOfWork;
        _clientRepository = clientRepository;
        _jwtService = jwtService;
        _domainBaseService = domainBaseService;
        _identityApplicationService = identityApplicationService;
    }

    public async Task<AddClientResponseModel?> AddClientAsync(AddClientRequestModel clientInfo)
    {
        if (await _identityApplicationService.GetUserByEmailAsync(clientInfo.User.Email) != null)
        {
            _notificator.AddNotification("Email is already registered", NotificationType.BusinessRules);
            return null;
        }

        if (await _clientRepository.GetAsync(x => x.CPF == clientInfo.CPF, true) != null)
        {
            _notificator.AddNotification("The CPF is being used by another user", NotificationType.BusinessRules);
            return null;
        }

        var identityUser =
            _identityApplicationService.GenerateIdentityUser(clientInfo.User.Email, true);

        await _identityApplicationService.BeginTransactionAsync();

        if (!await _identityApplicationService.CreateUserAsync(identityUser, clientInfo.User.Password))
            return null;

        await _identityApplicationService.CreateRoleAsync("Client");

        await _identityApplicationService.AddRoleToUserAsync(identityUser, "Client");

        var client = await CreateClientAsync(clientInfo);

        if (!_domainBaseService.IsEntityValid(new ClientValidation(), client))
        {
            await _identityApplicationService.RollbackChangesAsync();

            return null;
        }

        await _clientRepository.AddAsync(client);

        if (!await _unitOfWork.CommitAsync())
        {
            _notificator.AddNotification("Any changes were done at the database", NotificationType.IncorrectData);

            await _identityApplicationService.RollbackChangesAsync();

            return null;
        }

        await _identityApplicationService.CommitChangesAsync();

        var clientResponse = _mapper.Map<AddClientResponseModel>(client);

        clientResponse.AccessToken = await _jwtService.GenerateJwt(clientInfo.User.Email);

        return clientResponse;
    }

    private async Task<Client> CreateClientAsync(AddClientRequestModel clientInfo)
    {
        var client = _mapper.Map<Client>(clientInfo);

        client.User!.SetIdentityId(
            Guid.Parse((await _identityApplicationService.GetUserByEmailAsync(clientInfo.User.Email))!.Id)
        );

        return client;
    }
}