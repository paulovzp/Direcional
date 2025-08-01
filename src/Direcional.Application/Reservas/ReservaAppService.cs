﻿using Direcional.Application.Common;
using Direcional.Domain;
using Direcional.Infrastructure.Interfaces;
using System.Linq.Expressions;

namespace Direcional.Application;

public class ReservaAppService :
    DirecionalAppService<Reserva, ReservaResponse, ReservaReadResponse, ReservaCreateRequest, ReservaUpdateRequest, ReservaFilterRequest>
    , IReservaAppService
{
    private readonly IUserSession _userSession;
    private IReservaService _reservaService => (IReservaService)_service;
    public ReservaAppService(IReservaService service,
        IReservaRepository repository,
        IDirecionalUnitOfWork unitOfWork,
        IUserSession userSession)
        : base(service, repository, unitOfWork)
    {
        _userSession = userSession;
    }

    public override List<Expression<Func<Reserva, bool>>> GetFilter(FilterRequest<ReservaFilterRequest> request)
    {
        var expressions = new List<Expression<Func<Reserva, bool>>>();
        if (request.Filter is null)
            return expressions;

        if (request.Filter.CorretorId.HasValue)
            expressions.Add(func => func.CorretorId == request.Filter.CorretorId.Value);

        if (request.Filter.ClienteId.HasValue)
            expressions.Add(func => func.ClienteId == request.Filter.ClienteId.Value);

        if (request.Filter.ApartamentoId.HasValue)
            expressions.Add(func => func.ApartamentoId == request.Filter.ApartamentoId.Value);

        if (request.Filter.DataReservaInicio.HasValue)
            expressions.Add(func => func.DataReserva >= request.Filter.DataReservaInicio.Value.Date);

        if (request.Filter.DataReservaFim.HasValue)
            expressions.Add(func => func.DataReserva <= request.Filter.DataReservaFim.Value.Date);

        return expressions;
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
        var corretorId = _userSession.Id;
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