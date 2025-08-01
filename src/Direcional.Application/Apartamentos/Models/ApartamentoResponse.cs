namespace Direcional.Application;

public record ApartamentoDisponivel(string Message);

public class ApartamentoResponse
{
    public int Id { get; set; }
    public int Numero { get; set; }
    public int Andar { get; set; }
    public decimal ValorVenda { get; set; }
}

public class ApartamentoReadResponse
{
    public int Id { get; set; }
    public int Numero { get; set; }
    public int Andar { get; set; }
    public decimal ValorVenda { get; set; }
}

public class ApartamentoCreateRequest
{
    public int Numero { get; set; }
    public int Andar { get; set; }
    public decimal ValorVenda { get; set; }
}

public class ApartamentoUpdateRequest
{
    public decimal ValorVenda { get; set; }

}

public class ApartamentoFilterRequest
{
    public int? Numero { get; set; }
    public int? Andar { get; set; }
    public decimal? ValorVendaInicio { get; set; }
    public decimal? ValorVendaFim { get; set; }
}