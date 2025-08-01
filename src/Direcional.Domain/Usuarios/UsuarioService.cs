namespace Direcional.Domain;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _repository;

    public UsuarioService(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task Add(Usuario usuario)
    {
        string password = "P@ssw0rd"; // TO DO: Implementar gerador de codigo e enviar por email
        usuario.DefinirSenha(password);
        await _repository.Add(usuario);
    }
}
