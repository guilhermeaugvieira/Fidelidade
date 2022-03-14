using AutoMapper;
using FidelidadeBE.Application.Interfaces;
using FidelidadeBE.Business.Entities;
using FidelidadeBE.Core.Interfaces;
using FidelidadeBE.Core.Notifications;
using FidelidadeBE.Data.Interfaces;
using FidelidadeBE.Infra.Interfaces;
using FidelidadeBE.Business.Entities.Validations;
using FidelidadeBE.Business.Interfaces;
using FidelidadeBE.Business.Models.User;

namespace FidelidadeBE.Application.Services;

public class AdministratorApplicationService : IAdministratorApplicationService
{
    private readonly IMapper _mapper;
    private readonly INotificator _notificator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;
    private readonly IDomainBaseService _domainBaseService;
    private readonly IUserRepository _userRepository;
    private readonly IIdentityApplicationService _identityApplicationService;

    public AdministratorApplicationService(
        IMapper mapper,
        INotificator notificator,
        IUnitOfWork unitOfWork,
        IJwtService jwtService,
        IDomainBaseService domainBaseService,
        IUserRepository userRepository,
        IIdentityApplicationService identityApplicationService
    )
    {
        _mapper = mapper;
        _notificator = notificator;
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
        _domainBaseService = domainBaseService;
        _userRepository = userRepository;
        _identityApplicationService = identityApplicationService;
    }
    public async Task<AddUserResponseModel?> AddAdministratorUserAsync(AddUserRequestModel administratorUser)
    {
        if (await _identityApplicationService.GetUserByEmailAsync(administratorUser.Email) != null)
        {
            _notificator.AddNotification("Email is already registered", NotificationType.BusinessRules);
            return null;
        }

        var identityUser =
            _identityApplicationService.GenerateIdentityUser(administratorUser.Email, true);

        await _identityApplicationService.BeginTransactionAsync();

        if (!await _identityApplicationService.CreateUserAsync(identityUser, administratorUser.Password))
            return null;

        await _identityApplicationService.CreateRoleAsync("Administrator");

        await _identityApplicationService.AddRoleToUserAsync(identityUser, "Administrator");

        var superUser = await CreateAdministratorUserAsync(administratorUser);

        if (!_domainBaseService.IsEntityValid(new UserValidation(), superUser))
        {
            await _identityApplicationService.RollbackChangesAsync();
            return null;
        }

        await _userRepository.AddAsync(superUser);

        if (!await _unitOfWork.CommitAsync())
        {
            _notificator.AddNotification("Any changes were done at the database", NotificationType.IncorrectData);

            await _identityApplicationService.RollbackChangesAsync();

            return null;
        }

        await _identityApplicationService.CommitChangesAsync();

        #region Prepare Response

        var userResponse = _mapper.Map<AddUserResponseModel>(superUser);

        userResponse.AccessToken = await _jwtService.GenerateJwt(administratorUser.Email);

        #endregion

        return userResponse;
    }

    private async Task<User> CreateAdministratorUserAsync(AddUserRequestModel administratorUserInfo)
    {
        var administratorUser = _mapper.Map<User>(administratorUserInfo);

        administratorUser.SetIdentityId(
            Guid.Parse((await _identityApplicationService.GetUserByEmailAsync(administratorUserInfo.Email))!.Id)
        );

        return administratorUser;
    }
}