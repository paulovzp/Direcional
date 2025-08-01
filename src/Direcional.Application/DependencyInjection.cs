using Direcional.Domain;
using Direcional.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Direcional.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDomain();
        services.AddPersistence(configuration);

        services.AddScoped<IApartamentoAppService, ApartamentoAppService>();
        services.AddScoped<IClienteAppService, ClienteAppService>();
        services.AddScoped<IReservaAppService, ReservaAppService>();
        services.AddScoped<IVendaAppService, VendaAppService>();
        services.AddScoped<ICorretorAppService, CorretorAppService>();
        services.AddScoped<IAuthAppService, AuthAppService>();

        return services;
    }
}
