using Direcional.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Direcional.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DirecionalDbContext>(config =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            config.UseSqlServer(connectionString);
            config.EnableSensitiveDataLogging();
        });

        services.AddScoped<IDirecionalUnitOfWork, DirecionalUnitOfWork>();
        services.AddScoped<IApartamentoRepository, ApartamentoRepository>();
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IReservaRepository, ReservaRepository>();
        services.AddScoped<IVendaRepository, VendaRepository>();
        services.AddScoped<IVendedorRepository, VendedorRepository>();
        return services;
    }
}