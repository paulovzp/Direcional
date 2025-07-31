namespace Direcional.Domain;

using Direcional.Infrastructure.Constants;
using Direcional.Infrastructure.Exceptions;
using FluentValidation;

public class DirecionalService<T> : IDirecionalService<T>
    where T : DirecionalEntity
{
    protected readonly IDirecionalRepository<T> _repository;
    private readonly IDirecionalValidator<T> _validator;

    public DirecionalService(IDirecionalRepository<T> repository,
        IDirecionalValidator<T> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<T> Add(T entity)
    {
        var validtor = await _validator
            .ValidateAsync(entity, op => op.IncludeRuleSets(ValidationRules.CreateRule));

        if (!validtor.IsValid)
            throw new DirecionalDomainException($"Erro(s) ao inserir {typeof(T)}",  validtor.Errors);

        await _repository.Add(entity);
        return entity;
    }

    public async Task Update(T entity)
    {
        var validtor = await _validator
                .ValidateAsync(entity, op => op.IncludeRuleSets(ValidationRules.UpdateRule));

        if (!validtor.IsValid)
            throw new DirecionalDomainException($"Erro(s) ao atualizar {typeof(T)}", validtor.Errors);

        await _repository.Update(entity);
    }

    public async Task Delete(T entity)
    {
        var validtor = await _validator
            .ValidateAsync(entity, op => op.IncludeRuleSets(ValidationRules.DeleteRule));

        if (!validtor.IsValid)
            throw new DirecionalDomainException($"Erro(s) ao excluir {typeof(T)}", validtor.Errors);

        await _repository.Remove(entity);
    }

    public async Task Delete(int id)
    {
        var entity = await _repository.Get(id);
        await Delete(entity);
    }

    public void Dispose() => _repository.Dispose();
}
