namespace Direcional.Domain;

public class VendaService : DirecionalService<Venda>, IVendaService
{
    public VendaService(IVendaRepository repository, IVendaValidator validator)
        : base(repository, validator)
    {
    }
}

public class VendaValidator : DirecionalValidator<Venda>, IVendaValidator
{
    public VendaValidator()
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