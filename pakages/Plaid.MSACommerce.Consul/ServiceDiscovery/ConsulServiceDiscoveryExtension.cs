using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Plaid.MSACommerce.Consul.ServiceDiscovery;

public static class ConsulServiceDiscoveryExtension
{
    public static IServiceCollection AddConsulDiscovery(this IServiceCollection services)
    {
        services.TryAddSingleton<IServiceDiscpvery, ConsulServiceDiscovery>();
        return services;
    }
}