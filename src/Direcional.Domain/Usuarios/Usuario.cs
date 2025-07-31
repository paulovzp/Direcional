using Direcional.Infrastructure.Enums;
using System.Text;
using System.Security.Cryptography;

namespace Direcional.Domain;

public class Usuario : DirecionalEntity
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public string HashPassword { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
    public TipoUsuario Tipo { get; set; } = TipoUsuario.Admin;
    public Vendedor Vendedor { get; set; }
    public Cliente Cliente { get; set; }

    public void SetPassword(string password)
    {
        Senha = password;
        Salt = SaltGenerator();
        HashPassword = HashGenerator(Salt, password);
    }

    private string HashGenerator(string salt, string password)
    {
        string saltedPassword = password + salt;
        byte[] data = Encoding.ASCII.GetBytes(saltedPassword);
        data = new HMACSHA256().ComputeHash(data);

        return Encoding.ASCII.GetString(data);
    }

    private string SaltGenerator()
    {
        byte[] buffer = new byte[16];
        RandomNumberGenerator.Fill(buffer);
        return Convert.ToBase64String(buffer);
    }

    public static class PropertyLength
    {
        public static int Nome => 150;
        public static int Email => 150;
        public static int Senha => 30;
        public static int HashPassword => 250;
        public static int Salt => 80;
    }
}
