using Microsoft.Extensions.DependencyInjection;
using Plaid.MSACommerce.CommonServiceClient.AspNetCore;
using Plaid.MSACommerce.Consul.ServiceDiscovery;

namespace Plaid.MSACommerce.CommonServiceClient;

public static class ServiceClientExtension
{
    public static void AddServiceClient<TServiceClient>(this IServiceCollection services,
        Action<ServiceClientOption> configureServiceClient, Action<HttpClient> configureHttpClient)
        where TServiceClient : class, IServiceClient
    {
        var serviceClientOption = new ServiceClientOption();
        configureServiceClient.Invoke(serviceClientOption);
        services.AddConsulDiscovery();
        services.AddLoadBalancer<TServiceClient>(serviceClientOption.LoadBalancingStrategy);
        services.AddHttpClient<TServiceClient>(configureHttpClient);
        services.AddScoped<TServiceClient>();
    }
}