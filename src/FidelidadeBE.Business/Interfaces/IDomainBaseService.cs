using FidelidadeBE.Business.Entities;
using FluentValidation;

namespace FidelidadeBE.Business.Interfaces;

public interface IDomainBaseService
{
    bool IsEntityValid<TValidator, TEntity>(TValidator validation, TEntity entity)
        where TValidator : AbstractValidator<TEntity> where TEntity : BaseEntity;
}