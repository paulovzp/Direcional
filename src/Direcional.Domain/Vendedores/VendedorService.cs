namespace Direcional.Domain;

public class VendedorService : DirecionalService<Vendedor>, IVendedorService
{
    public VendedorService(IVendedorRepository repository, IVendedorValidator validator)
        : base(repository, validator)
    {
    }
}

public class VendedorValidator : DirecionalValidator<Vendedor>, IVendedorValidator
{
    public VendedorValidator()
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