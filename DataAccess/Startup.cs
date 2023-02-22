using DataAccess.Data;
using DataAccess.Data.Repositories.Interfaces;
using DataAccess.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess;

public static class Startup
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<PremiseContractsContext>(options =>
            options.UseSqlServer(config.GetConnectionString("Default")));

        services.AddScoped<IContractsRepository, ContractsRepository>();
        services.AddScoped<IPremiseRepository, PremiseRepository>();
        services.AddScoped<IEquipmentRepository, EquipmentRepository>();

        return services;
    }
}