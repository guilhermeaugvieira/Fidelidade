using FidelidadeBE.Business.Entities;
using FidelidadeBE.Business.Interfaces;
using FidelidadeBE.Core.Interfaces;
using FidelidadeBE.Core.Notifications;
using FluentValidation;
using FluentValidation.Results;

namespace FidelidadeBE.Business.Services;

public class DomainBaseService : IDomainBaseService
{
    private readonly INotificator _notificator;

    public DomainBaseService(INotificator notificator)
    {
        _notificator = notificator;
    }

    private void Notify(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors) Notify(error.ErrorMessage);
    }

    private void Notify(string message)
    {
        _notificator.AddNotification(message, NotificationType.BusinessRules);
    }

    public bool IsEntityValid<TValidator, TEntity>(TValidator validation, TEntity entity)
        where TValidator : AbstractValidator<TEntity>
        where TEntity : BaseEntity
    {
        var validator = validation.Validate(entity);

        if (validator.IsValid) return true;

        Notify(validator);

        return false;
    }
}