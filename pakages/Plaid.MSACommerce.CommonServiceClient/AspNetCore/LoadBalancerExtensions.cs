using Microsoft.Extensions.DependencyInjection;

namespace Plaid.MSACommerce.CommonServiceClient.AspNetCore;

public static class LoadBalancerExtensions
{
    /// <summary>
    /// 扩展服务
    /// </summary>
    /// <param name="services">扩展服务</param>
    /// <param name="strategy">负载均衡服务</param>
    /// <typeparam name="T">约束 T必须是一个类型</typeparam>
    /// <returns></returns>
    public static IServiceCollection AddLoadBalancer<T>(this IServiceCollection services,
        LoadBalancingStrategy strategy) where T : class
    {
        //注入一个泛型负载均衡 单例模式 
        services.AddSingleton<ILoadBalancer<T>>(new LoadBalancer<T>(strategy));
        return services;
    }
}