namespace Direcional.Domain;

public class Vendedor : DirecionalEntity
{
    public string Codigo { get; set; } = string.Empty;
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime? DataFim { get; set; }

    public static class PropertyLength
    {
        public static int Codigo => 10;
    }
}
