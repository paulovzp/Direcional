namespace Direcional.Domain;

public class CorretorService : DirecionalService<Corretor>, ICorretorService
{
    private readonly IUsuarioService _usuarioService;

    public CorretorService(ICorretorRepository repository, 
        ICorretorValidator validator,
        IUsuarioService usuarioService)
        : base(repository, validator)
    {
        _usuarioService = usuarioService;
    }

    public override async Task<Corretor> Add(Corretor entity)
    {
        var cliente = await base.Add(entity);
        await _usuarioService.Add(entity.Usuario);
        return cliente;
    }

    public override async Task Update(Corretor entity)
    {
        await base.Update(entity);
        await _usuarioService.Add(entity.Usuario);
    }
}
