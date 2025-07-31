
namespace Direcional.Domain;

public interface IUsuarioService
{
    Task Add(Usuario usuario);
}

public interface IUsuarioRepository : IDirecionalRepository<Usuario>
{

}
