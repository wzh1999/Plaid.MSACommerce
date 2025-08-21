using Microsoft.Extensions.DependencyInjection;

namespace Plaid.MSACommerce.Consul.ServiceRegistration;

/// <summary>
/// 服务注册的扩展类
/// </summary>
public static class ConsulServiceRegistrationExtensions
{
    public static IServiceCollection AddConsulService(this IServiceCollection services)
    {
        return services;
    }
}