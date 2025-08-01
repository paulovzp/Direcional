namespace Direcional.Application;

public class ReservaResponse
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public string ClienteNome { get; set; }
    public int CorretorId { get; set; }
    public string CorretorNome { get; set; }
    public int ApartamentoId { get; set; }
    public int ApartamentoAndar { get; set; }
    public int ApartamentoNumero { get; set; }
    public DateTime DataReserva { get; set; }
    public DateTime? DataStatusAlterado { get; set; }
    public string Status { get; set; }

}

public class ReservaReadResponse
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public string ClienteNome { get; set; }
    public int CorretorId { get; set; }
    public string CorretorNome { get; set; }
    public int ApartamentoId { get; set; }
    public int ApartamentoAndar { get; set; }
    public int ApartamentoNumero { get; set; }
    public DateTime DataReserva { get; set; }
    public DateTime? DataStatusAlterado { get; set; }
    public string Status { get; set; }
}

public class ReservaCreateRequest
{
    public int ClienteId { get; set; }
    public int ApartamentoId { get; set; }
}

public class ReservaUpdateRequest
{
}

public class ReservaFilterRequest
{
    public int? CorretorId { get; set; }
}