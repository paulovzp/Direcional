namespace Direcional.Api.Endpoitns;

public static class EndpointExtensions
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapCorretorEndpoints();
        app.MapClienteEndpoints();
        app.MapReservaEndpoints();
        app.MapApartamentoEndpoints();
        app.MapVendaEndpoints();
        app.MapAuthEndpoints();
    }
}
