namespace Direcional.Domain;

public class Venda : DirecionalEntity
{
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; }
    public int ApartamentoId { get; set; }
    public Apartamento Apartamento { get; set; }
    public DateTime DataVenda { get; private set; } = DateTime.Now;
    public decimal Valor { get; set; }
    public int VendedorId { get; set; }
    public Vendedor Vendedor { get; set; }
}
