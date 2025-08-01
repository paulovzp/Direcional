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
    public Corretor Corretor { get; set; }
    public Cliente Cliente { get; set; }

    public void DefinirSenha(string password)
    {
        Senha = password;
        Salt = SaltGenerator();
        HashPassword = HashGenerator(Salt, password);
    }

    public bool ValidarSenha(string password)
    {
        if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
            return false;

        var hashPassword = HashGenerator(Salt, password);
        return HashPassword.Equals(hashPassword, StringComparison.OrdinalIgnoreCase);
    }

    private string HashGenerator(string salt, string password)
    {
        string saltedPassword = password + salt;
        using var sha = SHA256.Create();
        var hash = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword)));
        return hash;
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
