
namespace Direcional.Domain;

public interface IApartamentoService : IDirecionalService<Apartamento>
{
    Task<bool> Disponivel(int apartamentoId);
    Task<bool> Exists(int apartamentoId);
    Task MarcarVendido(Apartamento apartamento);
    Task<bool> Reservado(int apartamentoId);
    Task<bool> Vendido(int apartamentoId);
}

public interface IApartamentoValidator : IDirecionalValidator<Apartamento>
{
}