﻿using Direcional.Infrastructure.Constants;
using Direcional.Infrastructure.Validators;
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
        ComprouApartamento();
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
            .NotEmpty().WithMessage(MensagemValidacao.Cliente.EmailRequired)
            .NotNull().WithMessage(MensagemValidacao.Cliente.EmailRequired)
            .MaximumLength(Cliente.PropertyLength.Email).WithMessage(string.Format(MensagemValidacao.MaxLengthMessage, "Email", Cliente.PropertyLength.Email))
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
            .NotEmpty().WithMessage(MensagemValidacao.Cliente.NomeRequired)
            .NotNull().WithMessage(MensagemValidacao.Cliente.NomeRequired)
            .MaximumLength(Cliente.PropertyLength.Nome).WithMessage(string.Format(MensagemValidacao.MaxLengthMessage, "Nome", Cliente.PropertyLength.Nome));
    }

    private void Telefone()
    {
        RuleFor(x => x.Telefone)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(MensagemValidacao.Cliente.TelefoneRequired)
            .NotNull().WithMessage(MensagemValidacao.Cliente.TelefoneRequired)
            .MaximumLength(Cliente.PropertyLength.Telefone).WithMessage(string.Format(MensagemValidacao.MaxLengthMessage, "Telefone", Cliente.PropertyLength.Telefone));
    }

    private void ComprouApartamento()
    {
        RuleFor(cliente => cliente)
            .MustAsync(async (cliente, cancellation) =>
            {
                var existe = await _repository.Exists(x => x.Id == cliente.Id && x.Vendas.Any());
                return !existe;
            }).WithMessage(MensagemValidacao.Cliente.NaoPodeSerExcluido);
    }

    private void ReservouAparamento()
    {
        RuleFor(cliente => cliente)
            .MustAsync(async (cliente, cancellation) =>
            {
                var existe = await _repository.Exists(x => x.Id == cliente.Id && x.Reservas.Any());
                return !existe;
            }).WithMessage(MensagemValidacao.Cliente.NaoPodeSerExcluido);
    }
}