using Direcional.Infrastructure.Constants;
using FluentValidation;

namespace Direcional.Domain;

public class ReservaValidator : DirecionalValidator<Reserva>, IReservaValidator
{
    private readonly IClienteRepository _clienteRepository;
    private readonly ICorretorRepository _corretorRepository;
    private readonly IApartamentoService _apartamentoService;

    public ReservaValidator(IClienteRepository clienteRepository,
        ICorretorRepository corretorRepository,
        IApartamentoService apartamentoService)
    {
        _clienteRepository = clienteRepository;
        _corretorRepository = corretorRepository;
        _apartamentoService = apartamentoService;
    }

    public override void CreateRules()
    {
        Cliente();
        Corretor();
        Apartamento();
        ApartamentoNaoReservadoOuVendido();
    }

    public override void DeleteRules() { }

    public override void UpdateRules() { }

    private void ApartamentoNaoReservadoOuVendido()
    {
        RuleFor(apto => apto)
            .Cascade(CascadeMode.Stop)
            .MustAsync(async (reserva, cancellationToken) =>
            {
                return !await _apartamentoService.Vendido(reserva.ApartamentoId);
            }).WithMessage(MensagemValidacao.Reserva.Vendido)
            .MustAsync(async (reserva, cancellationToken) =>
            {
                return !await _apartamentoService.Reservado(reserva.ApartamentoId);
            }).WithMessage(MensagemValidacao.Reserva.Reservado);
    }

    private void Apartamento()
    {
        RuleFor(x => x.ApartamentoId)
            .Cascade(CascadeMode.Stop)
            .NotEqual(0)
            .WithMessage(MensagemValidacao.Reserva.ApartamentoRequired)
            .MustAsync(async (reserva, id, cancellationToken) =>
            {
                return await _apartamentoService.Exists(id);
            }).WithMessage(MensagemValidacao.Apartamento.NotFound);
    }

    private void Corretor()
    {
        RuleFor(x => x.CorretorId)
            .Cascade(CascadeMode.Stop)
            .NotEqual(0)
            .WithMessage(MensagemValidacao.Reserva.ApartamentoRequired)
            .MustAsync(async (reserva, id, cancellationToken) =>
            {
                return await _corretorRepository.Exists(x => x.Id == id);
            }).WithMessage(MensagemValidacao.Corretor.NotFound);
    }

    private void Cliente()
    {
        RuleFor(x => x.ClienteId)
            .Cascade(CascadeMode.Stop)
            .NotEqual(0)
            .WithMessage(MensagemValidacao.Reserva.ApartamentoRequired)
            .MustAsync(async (reserva, id, cancellationToken) =>
            {
                return await _clienteRepository.Exists(x => x.Id == id);
            }).WithMessage(MensagemValidacao.Cliente.NotFound);
    }
}