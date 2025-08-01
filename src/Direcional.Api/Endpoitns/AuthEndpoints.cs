using Direcional.Application.Common;
using Direcional.Application;
using Microsoft.AspNetCore.Mvc;

namespace Direcional.Api.Endpoitns;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        app.MapGroup("api/auth")
            .MapPost("login", async ([FromBody] UserAuthRequest authRequest, IAuthAppService appService) =>
            {
                if (authRequest is null)
                    return Results.BadRequest("Auth request cannot be null.");

                var response = await appService.Authenticate(authRequest);
                return Results.Ok(response);
            })
            .Produces<UserAuthResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithTags("Auth");
    }
}
