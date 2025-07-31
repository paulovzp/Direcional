namespace Direcional.Application;

public class VendedorResponse
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
}

public class VendedorReadResponse
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
}

public class VendedorCreateRequest
{
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
}

public class VendedorUpdateRequest
{
    public string Telefone { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
}

public class VendedorFilterRequest
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
}