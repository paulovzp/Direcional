
namespace Direcional.Domain;

public class ClienteService : DirecionalService<Cliente>, IClienteService
{
    private readonly IUsuarioService _usuarioService;

    public ClienteService(IClienteRepository repository,
        IClienteValidator validator,
        IUsuarioService usuarioService)
        : base(repository, validator)
    {
        _usuarioService = usuarioService;
    }

    public override async Task<Cliente> Add(Cliente entity)
    {
        var cliente = await base.Add(entity);
        await _usuarioService.Add(entity.Usuario);
        return cliente;
    }

    public override async Task Update(Cliente entity)
    {
        await base.Update(entity);
        await _usuarioService.Add(entity.Usuario);
    }
}
