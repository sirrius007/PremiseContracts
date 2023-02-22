using Microsoft.Extensions.DependencyInjection;
using PremiseContractsService.Interfaces;
using PremiseContractsService.Mapping;

namespace PremiseContractsService;

public static class Startup
{
    public static IServiceCollection AddPremiseContractsService(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ContractProfile));
        services.AddTransient<IPremiseContractsService, PremiseContractsService>();
        return services;
    }
}