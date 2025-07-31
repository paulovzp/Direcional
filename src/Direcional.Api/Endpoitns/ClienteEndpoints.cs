using Direcional.Application;
using Direcional.Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace Direcional.Api.Endpoitns;

public static class ClienteEndpoints
{
    public static void MapClienteEndpoints(this WebApplication app)
    {
        app.MapGroup("api/cliente")
            .MapPost("paginate/list", async ([FromBody] FilterRequest<ClienteFilterRequest> filterRequest, IClienteAppService appService) =>
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
            .WithTags("Cliente")
            //.RequireAuthorization()
            ;

        app.MapGroup("api/cliente")
            .MapGet("{id}", async ([FromRoute] int id, IClienteAppService appService) =>
            {
                var response = await appService.Read(id);
                return Results.Ok(response);
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status422UnprocessableEntity)
            .WithTags("Cliente")
            //.RequireAuthorization()
            ;

        app.MapGroup("api/cliente")
            .MapPost("", async ([FromBody] ClienteCreateRequest request, IClienteAppService appService) =>
            {
                var id = await appService.Add(request);
                return Results.Ok(id);
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status422UnprocessableEntity)
            .WithTags("Cliente")
            //.RequireAuthorization()
            ;

        app.MapGroup("api/cliente")
            .MapPut("{id}", async ([FromRoute] int id, [FromBody] ClienteUpdateRequest request, IClienteAppService appService) =>
            {
                await appService.Update(id, request);
                return Results.NoContent();
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status422UnprocessableEntity)
            .WithTags("Cliente")
            //.RequireAuthorization()
            ;

        app.MapGroup("api/cliente")
            .MapDelete("{id}", async ([FromRoute] int id, IClienteAppService appService) =>
            {
                await appService.Delete(id);
                return Results.NoContent();
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status422UnprocessableEntity)
            .WithTags("Cliente")
            //.RequireAuthorization()
            ;
    }
}
