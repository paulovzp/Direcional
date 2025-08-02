using Direcional.Infrastructure.Enums;

namespace Direcional.Domain;

public class Reserva : DirecionalEntity
{
    public int ClienteId { get; private set; }
    public Cliente Cliente { get; private set; }
    public int ApartamentoId { get; private set; }
    public Apartamento Apartamento { get; private set; }
    public int CorretorId { get; private set; }
    public Corretor Corretor { get; private set; }
    public ReservaStatus Status { get; private set; } = ReservaStatus.Pendente;
    public DateTime DataReserva { get; private set; } = DateTime.Now;
    public DateTime? DataStatusAlterado { get; private set; } = DateTime.Now;

    private void UpdateStatus(ReservaStatus status)
    {
        Status = status;
        DataStatusAlterado = DateTime.Now;
    }

    public void Cancelar()
    {
        UpdateStatus(ReservaStatus.Cancelada);
    }

    public void Confirmar()
    {
        UpdateStatus(ReservaStatus.Confirmada);
    }

    public static Reserva Create(int clienteId, int apartamentoId, int corretorId)
    {
        return new Reserva
        {
            ClienteId = clienteId,
            ApartamentoId = apartamentoId,
            CorretorId = corretorId,
        };
    }


}
