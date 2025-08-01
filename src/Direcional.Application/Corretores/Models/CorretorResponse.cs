namespace Direcional.Application;

public class CorretorResponse
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
}

public class CorretorReadResponse
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
}

public class CorretorCreateRequest
{
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
}

public class CorretorUpdateRequest
{
    public string Telefone { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
}

public class CorretorFilterRequest
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string? Codigo { get; set; }
}