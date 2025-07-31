using FluentValidation;

namespace Direcional.Domain;

public interface IDirecionalValidator<T> : IValidator<T>
        where T : DirecionalEntity
{
}
