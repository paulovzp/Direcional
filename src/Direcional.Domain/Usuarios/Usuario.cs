namespace Direcional.Domain;

public class Usuario : DirecionalEntity
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public string HashPassword { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Celular { get; set; } = string.Empty;

    public static class PropertyLength
    {
        public static int Nome => 150;
        public static int Email => 150;
        public static int Senha => 30;
        public static int HashPassword => 250;
        public static int Salt => 80;
        public static int Telefone => 30;
        public static int Celular => 30;
    }
}
