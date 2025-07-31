namespace Direcional.Domain;

public class Apartamento : DirecionalEntity
{
    public int Numero { get; set; }
    public int Andar { get; set; }
    public ICollection<Reserva> Reservas { get; private set; } = [];
    public ICollection<Venda> Vendas { get; private set; } = [];

    public static class PropertyLength
    {
        public static int Endereco => 150;
    }
}
