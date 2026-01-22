using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Plaid.MSACommerce.Consul.ServiceDiscovery;
/// <summary>
/// 扩展方法,用于向DI容器注册Consul服务发现
/// </summary>
public static class ConsulServiceDiscoveryExtension
{
    /// <summary>
    /// 向DI容器注册Consul服务发现
    /// </summary>
    /// <param name="services">扩展方法上下文</param>
    /// <returns></returns>
    public static IServiceCollection AddConsulDiscovery(this IServiceCollection services)
    {
        //将服务发现注册为单例
        services.TryAddSingleton<IServiceDiscpvery, ConsulServiceDiscovery>();
        return services;
    }
}