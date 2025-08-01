namespace Direcional.Domain;

public class Venda : DirecionalEntity
{
    public int ClienteId { get; private set; }
    public Cliente Cliente { get; private set; }
    public int ApartamentoId { get; private set; }
    public Apartamento Apartamento { get; private set; }
    public DateTime DataVenda { get; private set; } = DateTime.Now;
    public decimal Valor { get; private set; }
    public decimal ValorEntrada { get; private set; }
    public int CorretorId { get; set; }
    public Corretor Corretor { get; private set; }

    public static Venda Create(
        int clienteId,
        int apartamentoId,
        decimal valor,
        decimal valorEntrada,
        int corretorId)
    {
        return new Venda
        {
            ClienteId = clienteId,
            ApartamentoId = apartamentoId,
            Valor = valor,
            ValorEntrada = valorEntrada,
            CorretorId = corretorId
        };
    }
}
