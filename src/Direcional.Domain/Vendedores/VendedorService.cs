namespace Direcional.Domain;

public class VendedorService : DirecionalService<Vendedor>, IVendedorService
{
    private readonly IUsuarioService _usuarioService;

    public VendedorService(IVendedorRepository repository, 
        IVendedorValidator validator,
        IUsuarioService usuarioService)
        : base(repository, validator)
    {
        _usuarioService = usuarioService;
    }

    public override async Task<Vendedor> Add(Vendedor entity)
    {
        var cliente = await base.Add(entity);
        await _usuarioService.Add(entity.Usuario);
        return cliente;
    }

    public override async Task Update(Vendedor entity)
    {
        await base.Update(entity);
        await _usuarioService.Add(entity.Usuario);
    }
}
