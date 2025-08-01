using Direcional.Application;
using Direcional.Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace Direcional.Api.Endpoitns;

public static class ApartamentoEndpoints
{
    public static void MapApartamentoEndpoints(this WebApplication app)
    {
        app.MapGroup("api/apartamento")
            .MapPost("paginate/list", async ([FromBody] FilterRequest<ApartamentoFilterRequest> filterRequest, IApartamentoAppService appService) =>
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
            .WithTags("Apartamento")
            .RequireAuthorization()
            ;

        app.MapGroup("api/apartamento")
            .MapGet("{id}", async ([FromRoute] int id, IApartamentoAppService appService) =>
            {
                var response = await appService.Read(id);
                return Results.Ok(response);
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status422UnprocessableEntity)
            .WithTags("Apartamento")
            .RequireAuthorization()
            ;

        app.MapGroup("api/apartamento")
            .MapGet("{id}/disponivel", async ([FromRoute] int id, IApartamentoAppService appService) =>
            {
                var response = await appService.Disponivel(id);
                return Results.Ok(response);
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags("Apartamento")
            .RequireAuthorization()
            ;

        app.MapGroup("api/apartamento")
            .MapPost("", async ([FromBody] ApartamentoCreateRequest request, IApartamentoAppService appService) =>
            {
                var id = await appService.Add(request);
                return Results.Ok(id);
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status422UnprocessableEntity)
            .WithTags("Apartamento")
            .RequireAuthorization()
            ;

        app.MapGroup("api/apartamento")
            .MapPut("{id}", async ([FromRoute] int id, [FromBody] ApartamentoUpdateRequest request, IApartamentoAppService appService) =>
            {
                await appService.Update(id, request);
                return Results.NoContent();
            })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status422UnprocessableEntity)
            .WithTags("Apartamento")
            .RequireAuthorization()
            ;

        app.MapGroup("api/apartamento")
            .MapDelete("{id}", async ([FromRoute] int id, IApartamentoAppService appService) =>
            {
                await appService.Delete(id);
                return Results.NoContent();
            })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status422UnprocessableEntity)
            .WithTags("Apartamento")
            .RequireAuthorization()
            ;
    }
}
