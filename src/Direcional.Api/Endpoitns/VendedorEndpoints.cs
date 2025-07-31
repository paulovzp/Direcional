using Direcional.Application.Common;
using Direcional.Application;
using Microsoft.AspNetCore.Mvc;

namespace Direcional.Api.Endpoitns;

public static class VendedorEndpoints
{
    public static void MapVendedorEndpoints(this WebApplication app)
    {
        app.MapGroup("api/vendedor")
            .MapPost("paginate/list", async ([FromBody] FilterRequest<VendedorFilterRequest> filterRequest, IVendedorAppService appService) =>
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
            .WithTags("Vendedor")
            //.RequireAuthorization()
            ;

        app.MapGroup("api/vendedor")
            .MapGet("{id}", async ([FromRoute] int id, IVendedorAppService appService) =>
            {
                var response = await appService.Read(id);
                return Results.Ok(response);
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status422UnprocessableEntity)
            .WithTags("Vendedor")
            //.RequireAuthorization()
            ;

        app.MapGroup("api/vendedor")
            .MapPost("", async ([FromBody] VendedorCreateRequest request, IVendedorAppService appService) =>
            {
                var id = await appService.Add(request);
                return Results.Ok(id);
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status422UnprocessableEntity)
            .WithTags("Vendedor")
            //.RequireAuthorization()
            ;

        app.MapGroup("api/vendedor")
            .MapPut("{id}", async ([FromRoute] int id, [FromBody] VendedorUpdateRequest request, IVendedorAppService appService) =>
            {
                await appService.Update(id, request);
                return Results.NoContent();
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status422UnprocessableEntity)
            .WithTags("Vendedor")
            //.RequireAuthorization()
            ;

        app.MapGroup("api/vendedor")
            .MapDelete("{id}", async ([FromRoute] int id, IVendedorAppService appService) =>
            {
                await appService.Delete(id);
                return Results.NoContent();
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status422UnprocessableEntity)
            .WithTags("Vendedor")
            //.RequireAuthorization()
            ;
    }
}
