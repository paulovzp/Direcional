using Direcional.Infrastructure.Constants;
using FluentValidation;

namespace Direcional.Domain;

public abstract class DirecionalValidator<T> : AbstractValidator<T>, IDirecionalValidator<T>
    where T : DirecionalEntity
{

    public DirecionalValidator()
    {
        RuleSet(ValidationRules.CreateRule, () =>
        {
            CreateRules();
        });

        RuleSet(ValidationRules.UpdateRule, () =>
        {
            UpdateRules();
        });

        RuleSet(ValidationRules.DeleteRule, () =>
        {
            DeleteRules();
        });
    }

    public abstract void CreateRules();
    public abstract void UpdateRules();
    public abstract void DeleteRules();
}