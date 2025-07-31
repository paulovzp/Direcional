namespace Direcional.Api.Endpoitns;

public static class EndpointExtensions
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapVendedorEndpoints();
        app.MapClienteEndpoints();
    }
}
