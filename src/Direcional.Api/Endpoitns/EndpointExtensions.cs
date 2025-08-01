namespace Direcional.Api.Endpoitns;

public static class EndpointExtensions
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapAuthEndpoints();
        app.MapCorretorEndpoints();
        app.MapClienteEndpoints();
        app.MapApartamentoEndpoints();
        app.MapReservaEndpoints();
        app.MapVendaEndpoints();
    }
}
