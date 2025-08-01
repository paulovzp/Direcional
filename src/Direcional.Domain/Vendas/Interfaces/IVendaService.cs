
namespace Direcional.Domain;

public interface IVendaService : IDirecionalService<Venda>
{
    Task Efetuar(Reserva reserva, decimal valorEntrada);
}

public interface IVendaValidator : IDirecionalValidator<Venda>
{
}