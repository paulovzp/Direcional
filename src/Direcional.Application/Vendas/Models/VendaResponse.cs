namespace Direcional.Application;

public class VendaResponse
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public string ClienteNome { get; set; }
    public int ApartamentoId { get; set; }
    public int ApartamentoAndar { get; set; }
    public int ApartamentoNumero { get; set; }
    public string ApartamentoNome { get; set; }
    public DateTime DataVenda { get; set; }
    public decimal Valor { get; set; }
    public decimal ValorEntrada { get; set; }
    public int CorretorId { get; set; }
    public string CorretorNome { get; set; }
}

public class VendaReadResponse
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public string ClienteNome { get; set; }
    public int ApartamentoId { get; set; }
    public int ApartamentoAndar { get; set; }
    public int ApartamentoNumero { get; set; }
    public string ApartamentoNome { get; set; }
    public DateTime DataVenda { get; set; }
    public decimal Valor { get; set; }
    public decimal ValorEntrada { get; set; }
    public int CorretorId { get; set; }
    public string CorretorNome { get; set; }
}

public class VendaCreateRequest
{
}

public class VendaUpdateRequest
{
}

public class VendaFilterRequest
{
    public DateTime? DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public int? CorretorId { get; set; }
    public int? ClienteId { get; set; }
    public int? ApartamentoId { get; set; }
}

public class PagamentoReservaRequest
{
    public decimal ValorEntrada { get; set; }
}