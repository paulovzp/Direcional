namespace Direcional.Domain;

public interface IReservaService : IDirecionalService<Reserva>
{
    Task Cancelar(Reserva reserva);
    Task MarcarConfirmar(Reserva reserva);
}

public interface IReservaValidator : IDirecionalValidator<Reserva>
{
}