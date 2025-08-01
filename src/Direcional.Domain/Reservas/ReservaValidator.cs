using Direcional.Infrastructure.Constants;
using Direcional.Infrastructure.Enums;
using FluentValidation;

namespace Direcional.Domain;

public class ReservaValidator : DirecionalValidator<Reserva>, IReservaValidator
{
    private readonly IReservaRepository _reservaRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IVendedorRepository _vendedorRepository;
    private readonly IApartamentoRepository _apartamentoRepository;
    private readonly IVendaRepository _vendaRepository;

    public ReservaValidator(IReservaRepository reservaRepository,
        IClienteRepository clienteRepository,
        IVendedorRepository vendedorRepository,
        IApartamentoRepository apartamentoRepository,
        IVendaRepository vendaRepository)
    {
        _reservaRepository = reservaRepository;
        _clienteRepository = clienteRepository;
        _vendedorRepository = vendedorRepository;
        _apartamentoRepository = apartamentoRepository;
        _vendaRepository = vendaRepository;
    }

    public override void CreateRules()
    {
        Cliente();
        Vendedor();
        Apartamento();
        ApartamentoNaoReservadoOuVendido();
    }

    public override void DeleteRules() { }

    public override void UpdateRules() { }

    private void ApartamentoNaoReservadoOuVendido()
    {
        RuleFor(apto=> apto)
            .Cascade(CascadeMode.Stop)
            .MustAsync(async (reserva, cancellationToken) =>
            {
                var isVendido = await _vendaRepository.Exists(x => x.ApartamentoId == reserva.ApartamentoId);
                return !isVendido;
            }).WithMessage(MensagemValidacao.Reserva.Vendido)
            .MustAsync(async (reserva, cancellationToken) =>
            {
                var isReservado = await _reservaRepository.Exists(x => x.ApartamentoId == reserva.ApartamentoId && x.Status != ReservaStatus.Cancelada && x.Status != ReservaStatus.Expirada);
                return !isReservado;
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
                return await _vendedorRepository.Exists(x => x.Id == id);
            }).WithMessage(MensagemValidacao.Apartamento.NotFound);
    }

    private void Vendedor()
    {
        RuleFor(x => x.VendedorId)
            .Cascade(CascadeMode.Stop)
            .NotEqual(0)
            .WithMessage(MensagemValidacao.Reserva.ApartamentoRequired)
            .MustAsync(async (reserva, id, cancellationToken) =>
            {
                return await _vendedorRepository.Exists(x => x.Id == id);
            }).WithMessage(MensagemValidacao.Vendedor.NotFound);
    }

    private void Cliente()
    {
        RuleFor(x => x.ClienteId)
            .Cascade(CascadeMode.Stop)
            .NotEqual(0)
            .WithMessage(MensagemValidacao.Reserva.ApartamentoRequired)
            .MustAsync(async (reserva, id, cancellationToken) =>
            {
                return await _vendedorRepository.Exists(x => x.Id == id);
            }).WithMessage(MensagemValidacao.Cliente.NotFound);
    }
}