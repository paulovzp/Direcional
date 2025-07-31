
using Direcional.Infrastructure.Constants;
using FluentValidation;

namespace Direcional.Domain;

public class ApartamentoValidator : DirecionalValidator<Apartamento>, IApartamentoValidator
{
    private readonly IApartamentoRepository _repository;

    public ApartamentoValidator(IApartamentoRepository repository)
    {
        _repository = repository;
    }

    public override void CreateRules()
    {
        Andar();
        Numero();
        ValorVenda();
        AptoAndarNumeroUnique();
    }

    public override void DeleteRules()
    {
        ApartamentoJaExisteMovimentos();
    }

    public override void UpdateRules()
    {
        Andar();
        Numero();
        ValorVenda();
        AptoAndarNumeroUnique();
    }

    private void ApartamentoJaExisteMovimentos()
    {
        RuleFor(x => x)
            .MustAsync(async (apartamento, cancellation) =>
            {
                return !await _repository
                .Exists(a => a.Id == apartamento.Id && (a.Reservas.Any() || a.Vendas.Any()));
            })
            .WithMessage(MensagemValidacao.Apartamento.NaoPodeSerExcluido);
    }

    private void AptoAndarNumeroUnique()
    {
        RuleFor(x => x)
            .MustAsync(async (apartamento, cancellation) =>
            {
                return !await _repository
                .Exists(a => a.Andar == apartamento.Andar && a.Numero == apartamento.Numero && a.Id != apartamento.Id);
            })
            .WithMessage(MensagemValidacao.Apartamento.AndarNumeroUnique);
    }

    private void ValorVenda()
    {
        RuleFor(x => x.ValorVenda)
            .GreaterThan(0)
            .WithMessage(MensagemValidacao.Apartamento.ValorVendaInvalid);
    }

    private void Numero()
    {
        RuleFor(x => x.Numero)
            .GreaterThan(0)
            .WithMessage(MensagemValidacao.Apartamento.NumeroRequired);
    }

    private void Andar()
    {
        RuleFor(x => x.Andar)
            .GreaterThan(-1)
            .WithMessage(MensagemValidacao.Apartamento.AndarRequired);
    }
}