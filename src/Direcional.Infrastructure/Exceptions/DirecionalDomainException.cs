using FluentValidation.Results;

namespace Direcional.Infrastructure.Exceptions;

public class DirecionalDomainException : Exception
{
    public IDictionary<string, string[]> errors { get; } = new Dictionary<string, string[]>();

    public DirecionalDomainException() : base()
    {
    }

    public DirecionalDomainException(string message)
        : base(message)
    {
    }

    public DirecionalDomainException(string message, Exception inner)
        : base(message, inner)
    {
    }

    public DirecionalDomainException(IDictionary<string, string[]> errors)
    {
        this.errors = errors;
    }

    public DirecionalDomainException(string message, List<ValidationFailure> validationFailures)
        : base(message)
    {
        var propertyNames = validationFailures.Select(e => e.PropertyName).Distinct();

        foreach (var propertyName in propertyNames)
        {
            var propertyFailures = validationFailures
                .Where(e => e.PropertyName == propertyName)
                .Select(e => e.ErrorMessage)
                .ToArray();

            errors.Add(propertyName, propertyFailures);
        }
    }
}
