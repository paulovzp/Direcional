using Direcional.Application;
using Direcional.Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace Direcional.Api.Endpoitns;

public static class VendaEndpoints
{
    public static void MapVendaEndpoints(this WebApplication app)
    {
        app.MapGroup("api/venda")
            .MapPost("paginate/list", async ([FromBody] FilterRequest<VendaFilterRequest> filterRequest, IVendaAppService appService) =>
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
            .WithTags("Venda")
            .RequireAuthorization(policy => policy.RequireRole("Corretor"));

        app.MapGroup("api/venda")
            .MapPost("{reservaId}/finalizar", async ([FromRoute] int reservaId, 
                [FromBody] PagamentoReservaRequest request, IVendaAppService appService) =>
            {
                await appService.Efetuar(reservaId, request);
                return Results.NoContent();
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status422UnprocessableEntity)
            .WithTags("Venda")
            .RequireAuthorization(policy => policy.RequireRole("Corretor"));
    }
}
