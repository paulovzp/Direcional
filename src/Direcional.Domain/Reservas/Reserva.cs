using Direcional.Infrastructure.Enums;

namespace Direcional.Domain;

public class Reserva : DirecionalEntity
{
    public int ClienteId { get; private set; }
    public Cliente Cliente { get; private set; }
    public int ApartamentoId { get; private set; }
    public Apartamento Apartamento { get; private set; }
    public int VendedorId { get; private set; }
    public Vendedor Vendedor { get; private set; }
    public ReservaStatus Status { get; private set; } = ReservaStatus.Pendente;
    public DateTime DataReserva { get; private set; } = DateTime.Now;
    public DateTime? DataStatusAlterado { get; private set; } = DateTime.Now;

    public void UpdateStatus(ReservaStatus status)
    {
        Status = status;
        DataStatusAlterado = DateTime.Now;
    }

    public static Reserva Create(int clienteId, int apartamentoId, int vendedorId)
    {
        return new Reserva
        {
            ClienteId = clienteId,
            ApartamentoId = apartamentoId,
            VendedorId = vendedorId,
        };
    }
}
