using Direcional.Infrastructure.Enums;

namespace Direcional.Domain;

public class Cliente : DirecionalEntity
{
    public string Nome { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Telefone { get; private set; } = string.Empty;
    public int UsuarioId { get; private set; }
    public Usuario Usuario { get; private set; }
    public ICollection<Reserva> Reservas { get; private set; } = [];
    public ICollection<Venda> Vendas { get; private set; } = [];

    public static class PropertyLength
    {
        public static int Nome => 150;
        public static int Email => 150;
        public static int Telefone => 30;
    }

    public void Update(string nome, string telefone)
    {
        Nome = nome;
        Telefone = telefone;
        if (Usuario is not null)
            Usuario.Nome = nome;
    }
    public static Cliente Create(string nome, string email, string telefone)
    {
        return new Cliente
        {
            Nome = nome,
            Email = email,
            Telefone = telefone,
            Usuario = new Usuario()
            {
                Nome = nome,
                Email = email,
                Tipo = TipoUsuario.Cliente,
            }
        };
    }
}
