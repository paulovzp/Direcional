using Direcional.Infrastructure.Constants;
using Direcional.Infrastructure.Validators;
using FluentValidation;

namespace Direcional.Domain;

public class CorretorValidator : DirecionalValidator<Corretor>, ICorretorValidator
{
    private readonly ICorretorRepository _repository;

    public CorretorValidator(ICorretorRepository repository)
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
        VendeuApartamento();
        ReservouAparamento();
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
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(MensagemValidacao.Corretor.EmailRequired)
            .NotNull().WithMessage(MensagemValidacao.Corretor.EmailRequired)
            .MaximumLength(Corretor.PropertyLength.Email).WithMessage(string.Format(MensagemValidacao.MaxLengthMessage, "Email", Corretor.PropertyLength.Email))
            .Must(email => EmailValidator.IsValid(email!)).WithMessage(MensagemValidacao.EmailInvalid)
            .MustAsync(async (cliente, email, cancellation) =>
            {
                var exists = await _repository.Exists(x => x.Email.ToUpper() == email.ToUpper() && x.Id != cliente.Id);
                return !exists;
            }).WithMessage(MensagemValidacao.EmailUniqueMessage);
    }

    private void Nome()
    {
        RuleFor(x => x.Nome)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(MensagemValidacao.Corretor.NomeRequired)
            .NotNull().WithMessage(MensagemValidacao.Corretor.NomeRequired)
            .MaximumLength(Corretor.PropertyLength.Nome).WithMessage(string.Format(MensagemValidacao.MaxLengthMessage, "Nome", Corretor.PropertyLength.Nome));
    }

    private void Telefone()
    {
        RuleFor(x => x.Telefone)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(MensagemValidacao.Corretor.TelefoneRequired)
            .NotNull().WithMessage(MensagemValidacao.Corretor.TelefoneRequired)
            .MaximumLength(Corretor.PropertyLength.Telefone).WithMessage(string.Format(MensagemValidacao.MaxLengthMessage, "Telefone", Corretor.PropertyLength.Telefone));
    }

    private void VendeuApartamento()
    {
        RuleFor(cliente => cliente)
            .MustAsync(async (cliente, cancellation) =>
            {
                var existe = await _repository.Exists(x => x.Id == cliente.Id && x.Vendas.Any());
                return !existe;
            }).WithMessage(MensagemValidacao.Corretor.NaoPodeSerExcluido);
    }

    private void ReservouAparamento()
    {
        RuleFor(cliente => cliente)
            .MustAsync(async (cliente, cancellation) =>
            {
                var existe = await _repository.Exists(x => x.Id == cliente.Id && x.Reservas.Any());
                return !existe;
            }).WithMessage(MensagemValidacao.Corretor.NaoPodeSerExcluido);
    }
}