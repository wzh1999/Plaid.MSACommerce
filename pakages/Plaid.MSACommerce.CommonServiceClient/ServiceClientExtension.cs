using Microsoft.Extensions.DependencyInjection;
using Plaid.MSACommerce.CommonServiceClient.AspNetCore;
using Plaid.MSACommerce.Consul.ServiceDiscovery;

namespace Plaid.MSACommerce.CommonServiceClient;
/// <summary>
/// 服务客户扩展服务 注册入口
/// </summary>
public static class ServiceClientExtension
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    /// <param name="services">扩展服务</param>
    /// <param name="configureServiceClient">配置委托 配置服务客户端</param>
    /// <param name="configureHttpClient">配置HttpClient</param>
    /// <typeparam name="TServiceClient"></typeparam>
    public static void AddServiceClient<TServiceClient>(this IServiceCollection services,
        Action<ServiceClientOption> configureServiceClient, Action<HttpClient> configureHttpClient)
        where TServiceClient : class, IServiceClient
    {
        //获取服务客户端负载均衡策略
        var serviceClientOption = new ServiceClientOption();
        configureServiceClient.Invoke(serviceClientOption);
        //注册服务发现
        services.AddConsulDiscovery();
        //注册泛型负载均衡器 表示为某一个客户端注册的 同时指定一个负载均衡策略
        services.AddLoadBalancer<TServiceClient>(serviceClientOption.LoadBalancingStrategy);
        //注册HttpClient
        services.AddHttpClient<TServiceClient>(configureHttpClient);
        //注入服务 作用域方式 因为是在构造函数中注入的 所以要保证一次请求中只能实例化一次
        services.AddScoped<TServiceClient>();
    }
}