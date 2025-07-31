using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace Direcional.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IApartamentoService, ApartamentoService>();
        services.AddScoped<IApartamentoValidator, ApartamentoValidator>();
        services.AddScoped<IClienteService, ClienteService>();
        services.AddScoped<IClienteValidator, ClienteValidator>();
        services.AddScoped<IReservaService, ReservaService>();
        services.AddScoped<IReservaValidator, ReservaValidator>();
        services.AddScoped<IVendaService, VendaService>();
        services.AddScoped<IVendaValidator, VendaValidator>();
        services.AddScoped<IVendedorService, VendedorService>();
        services.AddScoped<IVendedorValidator, VendedorValidator>();

        return services;
    }
}
