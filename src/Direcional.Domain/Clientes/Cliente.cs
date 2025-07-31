namespace Direcional.Domain;

public class Cliente : DirecionalEntity
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public ICollection<Cliente> Clientes { get; private set; } = [];
    public ICollection<Venda> Vendas { get; private set; } = [];

    public static class PropertyLength
    {
        public static int Nome => 150;
        public static int Email => 150;
        public static int Telefone => 30;
    }
}
