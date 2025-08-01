using Direcional.Application.Common;

namespace Direcional.Application;

public class ClienteResponse
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
}

public class ClienteReadResponse
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
}

public class ClienteCreateRequest
{
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
}

public class ClienteUpdateRequest
{
    public string Telefone { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
}

public class ClienteFilterRequest
{
    public int? Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
}