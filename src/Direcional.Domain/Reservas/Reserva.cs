using Direcional.Infrastructure.Enums;

namespace Direcional.Domain;

public class Reserva : DirecionalEntity
{
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; }
    public int ApartamentoId { get; set; }
    public Apartamento Apartamento { get; set; }
    public int VendedorId { get; set; }
    public Vendedor Vendedor { get; set; }
    public ReservaStatus Status { get; private set; } = ReservaStatus.Pendente;
    public DateTime DataReserva { get; private set; } = DateTime.Now;
}
