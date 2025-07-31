using Direcional.Infrastructure.Constants;
using FluentValidation;

namespace Direcional.Domain;

public class ClienteValidator : DirecionalValidator<Cliente>, IClienteValidator
{
    private readonly IClienteRepository _repository;

    public ClienteValidator(IClienteRepository repository)
    {
        _repository = repository;
    }

    public override void CreateRules()
    {
        Email();
        Nome();
        Telefone();
    }

    public override void DeleteRules()
    {
        Id();
    }

    public override void UpdateRules()
    {
        Email();
        Nome();
        Telefone();
    }

    private void Id()
    {
        RuleFor(x => x.Id)
            .NotEqual(0).WithMessage(MensagemValidacao.IdZerado);
    }

    private void Email()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(MensagemValidacao.Cliente.EmailRequired)
            .NotNull().WithMessage(MensagemValidacao.Cliente.EmailRequired)
            .MaximumLength(Cliente.PropertyLength.Email).WithMessage(string.Format(MensagemValidacao.MaxLengthMessage, "Email" ,Cliente.PropertyLength.Email))
            .MustAsync(async (cliente, email, cancellation) =>
            {
                var exists = await _repository.Exists(x => x.Email.ToUpper() == email.ToUpper() && x.Id != cliente.Id);
                return !exists;
            }).WithMessage(MensagemValidacao.EmailUniqueMessage);
    }

    private void Nome()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage(MensagemValidacao.Cliente.NomeRequired)
            .NotNull().WithMessage(MensagemValidacao.Cliente.NomeRequired)
            .MaximumLength(Cliente.PropertyLength.Nome).WithMessage(string.Format(MensagemValidacao.MaxLengthMessage, "Nome", Cliente.PropertyLength.Nome));
    }

    private void Telefone()
    {
        RuleFor(x => x.Telefone)
            .NotEmpty().WithMessage(MensagemValidacao.Cliente.TelefoneRequired)
            .NotNull().WithMessage(MensagemValidacao.Cliente.TelefoneRequired)
            .MaximumLength(Cliente.PropertyLength.Telefone).WithMessage(string.Format(MensagemValidacao.MaxLengthMessage, "Telefone", Cliente.PropertyLength.Telefone));
    }
}