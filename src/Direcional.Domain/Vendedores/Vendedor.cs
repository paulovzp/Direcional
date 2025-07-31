using Direcional.Infrastructure.Enums;
using System.Security.Cryptography;
using System.Text;

namespace Direcional.Domain;

public class Vendedor : DirecionalEntity
{
    public string Codigo { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    public DateTime DataInicio { get; private set; }
    public DateTime? DataFim { get; set; }

    public ICollection<Venda> Vendas { get; private set; } = [];
    public ICollection<Reserva> Reservas { get; private set; } = [];

    public static class PropertyLength
    {
        public static int Codigo => 10;
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

    public static Vendedor Create(string nome, string email, string telefone)
    {
        return new Vendedor
        {
            Codigo = GenerateCode(),
            Nome = nome,
            Email = email,
            Telefone = telefone,
            DataInicio = DateTime.Now,
            Usuario = new Usuario()
            {
                Nome = nome,
                Email = email,
                Tipo = TipoUsuario.Vendedor,
            }
        };
    }

    private static string GenerateCode()
    {
        const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string digits = "0123456789";
        StringBuilder result = new StringBuilder(6);

        byte[] buffer = new byte[6];
        RandomNumberGenerator.Fill(buffer);

        for (int i = 0; i < 3; i++)
        {
            int index = buffer[i] % letters.Length;
            result.Append(letters[index]);
        }

        for (int i = 3; i < 6; i++)
        {
            int index = buffer[i] % digits.Length;
            result.Append(digits[index]);
        }

        return result.ToString();
    }
}
