namespace Direcional.Domain;

public class ApartamentoService : DirecionalService<Apartamento>, IApartamentoService
{
    public ApartamentoService(IApartamentoRepository repository, IApartamentoValidator validator) 
        : base(repository, validator)
    {
    }
}


public class ApartamentoValidator : DirecionalValidator<Apartamento>, IApartamentoValidator
{
    public ApartamentoValidator()
    {
    }

    public override void CreateRules()
    {
        
    }

    public override void DeleteRules()
    {
        
    }

    public override void UpdateRules()
    {
        
    }
}