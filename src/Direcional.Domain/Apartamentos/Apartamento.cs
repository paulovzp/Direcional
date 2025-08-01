using Direcional.Infrastructure.Enums;

namespace Direcional.Domain;

public class Apartamento : DirecionalEntity
{
    public string Nome { get; set; }
    public int Numero { get; set; }
    public int Andar { get; set; }
    public decimal ValorVenda { get; set; }
    public ApartamentoStatus Status { get; private set; } = ApartamentoStatus.Disponivel;
    public ICollection<Reserva> Reservas { get; private set; } = [];
    public ICollection<Venda> Vendas { get; private set; } = [];

    public static class PropertyLength
    {
        public static int Nome => 150;
    }

    public void MarcarVendido()
    {
        Status = ApartamentoStatus.Vendido;
    }
}
