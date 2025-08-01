using Direcional.Application.Common;
using Direcional.Application;
using Microsoft.AspNetCore.Mvc;

namespace Direcional.Api.Endpoitns;

public static class ReservaEndpoints
{
    public static void MapReservaEndpoints(this WebApplication app)
    {
        app.MapGroup("api/reserva")
            .MapPost("paginate/list", async ([FromBody] FilterRequest<ReservaFilterRequest> filterRequest, IReservaAppService appService) =>
            {
                if (filterRequest is null)
                    return Results.BadRequest("Filter request cannot be null.");
                var response = await appService.Read(filterRequest);
                return Results.Ok(response);
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status403Forbidden)
            .WithTags("Reserva")
            .RequireAuthorization()
            ;

        app.MapGroup("api/reserva")
            .MapGet("{id}", async ([FromRoute] int id, IReservaAppService appService) =>
            {
                var response = await appService.Read(id);
                return Results.Ok(response);
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status422UnprocessableEntity)
            .WithTags("Reserva")
            .RequireAuthorization()
            ;

        app.MapGroup("api/reserva")
            .MapPost("", async ([FromBody] ReservaCreateRequest request, IReservaAppService appService) =>
            {
                var id = await appService.Add(request);
                return Results.Ok(id);
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status422UnprocessableEntity)
            .WithTags("Reserva")
            .RequireAuthorization()
            ;

        app.MapGroup("api/reserva")
            .MapPost("{id}/cancelar", async ([FromRoute] int id, IReservaAppService appService) =>
            {
                await appService.Cancelar(id);
                return Results.NoContent();
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status422UnprocessableEntity)
            .WithTags("Reserva")
            .RequireAuthorization()
            ;
    }
}
