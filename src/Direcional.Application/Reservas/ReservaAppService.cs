using Direcional.Application.Common;
using Direcional.Domain;
using System.Linq.Expressions;

namespace Direcional.Application;

public class ReservaAppService :
    DirecionalAppService<Reserva, ReservaResponse, ReservaReadResponse, ReservaCreateRequest, ReservaUpdateRequest, ReservaFilterRequest>
    , IReservaAppService
{
    private IReservaService _reservaService => (IReservaService) _service;
    public ReservaAppService(IReservaService service,
        IReservaRepository repository,
        IDirecionalUnitOfWork unitOfWork)
        : base(service, repository, unitOfWork)
    {
    }

    public override Expression<Func<Reserva, bool>> GetFilter(FilterRequest<ReservaFilterRequest> request)
    {
        return x => true;
    }

    public override Expression<Func<Reserva, object>> GetSort(string sortBy)
    {
        return sortBy.ToLower() switch
        {
            "clientenome" => func => func.Cliente.Nome,
            "corretorNome" => func => func.Corretor.Nome,
            "data" => func => func.DataReserva,
            _ => func => func.Id,
        };
    }

    public async Task Cancelar(int id)
    {
        var reserva = await ReadEntity(id);
        await _reservaService.Cancelar(reserva);
        await _unitOfWork.CommitAsync();
    }

    public override Reserva ToEntity(ReservaCreateRequest request)
    {
        //TO DO: Buscar da sessão do corretor logado
        var corretorId = 1;
        return Reserva.Create(request.ClienteId, request.ApartamentoId, corretorId);
    }

    public override Reserva ToEntity(ReservaUpdateRequest request, Reserva reserva)
    {
        return reserva;
    }

    public override IEnumerable<ReservaReadResponse> ToReadResponse(IEnumerable<Reserva> entities)
    {
        return entities.Select(x => new ReservaReadResponse
        {
            DataReserva = x.DataReserva,
            DataStatusAlterado = x.DataStatusAlterado,
            Id = x.Id,
            ClienteId = x.ClienteId,
            ClienteNome = x.Cliente.Nome,
            CorretorId = x.CorretorId,
            CorretorNome = x.Corretor.Nome,
            ApartamentoId = x.ApartamentoId,
            ApartamentoAndar = x.Apartamento.Andar,
            ApartamentoNumero = x.Apartamento.Numero,
            Status = x.Status.ToString()
        });
    }

    public override ReservaResponse ToResponse(Reserva entity)
    {
        return new ReservaResponse
        {
            DataReserva = entity.DataReserva,
            DataStatusAlterado = entity.DataStatusAlterado,
            Id = entity.Id,
            ClienteId = entity.ClienteId,
            ClienteNome = entity.Cliente.Nome,
            CorretorId = entity.CorretorId,
            CorretorNome = entity.Corretor.Nome,
            ApartamentoId = entity.ApartamentoId,
            ApartamentoAndar = entity.Apartamento.Andar,
            ApartamentoNumero = entity.Apartamento.Numero,
            Status = entity.Status.ToString()
        };
    }
}