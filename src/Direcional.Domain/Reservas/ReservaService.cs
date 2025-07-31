namespace Direcional.Domain;

public class ReservaService : DirecionalService<Reserva>, IReservaService
{
    public ReservaService(IReservaRepository repository, IReservaValidator validator)
        : base(repository, validator)
    {
    }
}

public class ReservaValidator : DirecionalValidator<Reserva>, IReservaValidator
{
    public ReservaValidator()
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