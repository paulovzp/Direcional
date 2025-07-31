namespace Direcional.Domain;

public class ClienteService : DirecionalService<Cliente>, IClienteService
{
    public ClienteService(IClienteRepository repository, IClienteValidator validator)
        : base(repository, validator)
    {
    }
}
