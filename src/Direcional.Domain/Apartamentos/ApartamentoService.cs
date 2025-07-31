namespace Direcional.Domain;

public class ApartamentoService : DirecionalService<Apartamento>, IApartamentoService
{
    public ApartamentoService(IApartamentoRepository repository, IApartamentoValidator validator) 
        : base(repository, validator)
    {
    }
}
