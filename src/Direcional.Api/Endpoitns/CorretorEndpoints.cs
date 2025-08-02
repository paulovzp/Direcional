using Direcional.Application.Common;
using Direcional.Application;
using Microsoft.AspNetCore.Mvc;

namespace Direcional.Api.Endpoitns;

public static class CorretorEndpoints
{
    public static void MapCorretorEndpoints(this WebApplication app)
    {
        app.MapGroup("api/corretor")
            .MapPost("paginate/list", async ([FromBody] FilterRequest<CorretorFilterRequest> filterRequest, ICorretorAppService appService) =>
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
            .WithTags("Corretor")
            .RequireAuthorization();

        app.MapGroup("api/corretor")
            .MapGet("{id}", async ([FromRoute] int id, ICorretorAppService appService) =>
            {
                var response = await appService.Read(id);
                return Results.Ok(response);
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status422UnprocessableEntity)
            .WithTags("Corretor")
            .RequireAuthorization();

        app.MapGroup("api/corretor")
            .MapPost("", async ([FromBody] CorretorCreateRequest request, ICorretorAppService appService) =>
            {
                var id = await appService.Add(request);
                return Results.Ok(id);
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status422UnprocessableEntity)
            .WithTags("Corretor")
            .RequireAuthorization(policy => policy.RequireRole("Admin"));

        app.MapGroup("api/corretor")
            .MapPut("{id}", async ([FromRoute] int id, [FromBody] CorretorUpdateRequest request, ICorretorAppService appService) =>
            {
                await appService.Update(id, request);
                return Results.NoContent();
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status422UnprocessableEntity)
            .WithTags("Corretor")
            .RequireAuthorization(policy => policy.RequireRole("Admin"));

        app.MapGroup("api/corretor")
            .MapDelete("{id}", async ([FromRoute] int id, ICorretorAppService appService) =>
            {
                await appService.Delete(id);
                return Results.NoContent();
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status422UnprocessableEntity)
            .WithTags("Corretor")
            .RequireAuthorization(policy => policy.RequireRole("Admin"));
    }
}
