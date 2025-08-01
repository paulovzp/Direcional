using Direcional.Application.Common;
using Direcional.Domain;
using System.Linq.Expressions;

namespace Direcional.Application;

public class CorretorAppService :
    DirecionalAppService<Corretor, CorretorResponse, CorretorReadResponse, CorretorCreateRequest, CorretorUpdateRequest, CorretorFilterRequest>
    , ICorretorAppService
{
    public CorretorAppService(ICorretorService service,
        ICorretorRepository repository,
        IDirecionalUnitOfWork unitOfWork)
        : base(service, repository, unitOfWork)
    {
    }

    public override Expression<Func<Corretor, bool>> GetFilter(FilterRequest<CorretorFilterRequest> request)
    {
        return x => true;
    }

    public override Expression<Func<Corretor, object>> GetSort(string sortBy)
    {
        return sortBy.ToLower() switch
        {
            "nome" => func => func.Nome,
            "telefone" => func => func.Telefone,
            "email" => func => func.Email,
            "codigo" => func => func.Codigo,
            _ => func => func.Id,
        };
    }

    public override Corretor ToEntity(CorretorCreateRequest request)
        => Corretor.Create(request.Nome, request.Email, request.Telefone);

    public override Corretor ToEntity(CorretorUpdateRequest request, Corretor corretor)
    {
        corretor.Update(request.Nome, request.Telefone);
        return corretor;
    }

    public override IEnumerable<CorretorReadResponse> ToReadResponse(IEnumerable<Corretor> entities)
    {
        return entities.Select(x => new CorretorReadResponse
        {
            Telefone = x.Telefone,
            Nome = x.Nome,
            Codigo = x.Codigo,
            Email = x.Email,
            DataInicio = x.DataInicio,
            DataFim = x.DataFim,
            Id = x.Id
        });
    }

    public override CorretorResponse ToResponse(Corretor entity)
    {
        return new CorretorResponse
        {
            Telefone = entity.Telefone,
            Nome = entity.Nome,
            Codigo = entity.Codigo,
            Email = entity.Email,
            DataInicio = entity.DataInicio,
            DataFim = entity.DataFim,
            Id = entity.Id
        };
    }
}