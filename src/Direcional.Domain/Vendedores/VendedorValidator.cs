using Direcional.Infrastructure.Constants;
using Direcional.Infrastructure.Validators;
using FluentValidation;

namespace Direcional.Domain;

public class VendedorValidator : DirecionalValidator<Vendedor>, IVendedorValidator
{
    private readonly IVendedorRepository _repository;

    public VendedorValidator(IVendedorRepository repository)
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
            .NotEmpty().WithMessage(MensagemValidacao.Vendedor.EmailRequired)
            .NotNull().WithMessage(MensagemValidacao.Vendedor.EmailRequired)
            .MaximumLength(Vendedor.PropertyLength.Email).WithMessage(string.Format(MensagemValidacao.MaxLengthMessage, "Email", Vendedor.PropertyLength.Email))
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
            .NotEmpty().WithMessage(MensagemValidacao.Vendedor.NomeRequired)
            .NotNull().WithMessage(MensagemValidacao.Vendedor.NomeRequired)
            .MaximumLength(Vendedor.PropertyLength.Nome).WithMessage(string.Format(MensagemValidacao.MaxLengthMessage, "Nome", Vendedor.PropertyLength.Nome));
    }

    private void Telefone()
    {
        RuleFor(x => x.Telefone)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(MensagemValidacao.Vendedor.TelefoneRequired)
            .NotNull().WithMessage(MensagemValidacao.Vendedor.TelefoneRequired)
            .MaximumLength(Vendedor.PropertyLength.Telefone).WithMessage(string.Format(MensagemValidacao.MaxLengthMessage, "Telefone", Vendedor.PropertyLength.Telefone));
    }

    private void VendeuApartamento()
    {
        RuleFor(cliente => cliente)
            .MustAsync(async (cliente, cancellation) =>
            {
                var existe = await _repository.Exists(x => x.Id == cliente.Id && x.Vendas.Any());
                return !existe;
            }).WithMessage(MensagemValidacao.Vendedor.NaoPodeSerExcluido);
    }

    private void ReservouAparamento()
    {
        RuleFor(cliente => cliente)
            .MustAsync(async (cliente, cancellation) =>
            {
                var existe = await _repository.Exists(x => x.Id == cliente.Id && x.Reservas.Any());
                return !existe;
            }).WithMessage(MensagemValidacao.Vendedor.NaoPodeSerExcluido);
    }
}