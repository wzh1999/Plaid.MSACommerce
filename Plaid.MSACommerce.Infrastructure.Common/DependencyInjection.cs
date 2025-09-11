using Consul.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Plaid.MSACommerce.Consul.ServiceRegistration;

namespace Plaid.MSACommerce.Infrastructure.Common;

public static class DependencyInjection
{
    /// <summary>
    /// 定义扩展方法
    /// </summary>
    /// <param name="services">服务扩展</param>
    /// <param name="configuration">配置管理</param>
    /// <returns></returns>
    public static IServiceCollection AddInfrastructureCommon(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigureConsul(services, configuration);
        return services;
    }
    
    /// <summary>
    /// 用于统一配置Consul 服务注册处理逻辑 共用其他微服务直接依赖于这个公共底层处理逻辑，无需在多个地方进行通过注册入口
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void ConfigureConsul(IServiceCollection services, IConfiguration configuration)
    {
        
        var configurationSection = configuration.GetSection("ServiceCheck");
        var serviceCheck = configurationSection.Get<ServiceCheckConfiguration>();
        services.Configure<ServiceConfiguration>(configurationSection);

        services.AddConsul();
        services.AddConsulService(serviceConfiguration =>
        {
            serviceConfiguration.ServiceAddress = new Uri(configuration["urls"]??configuration["applicationUrl"]);
        }, serviceCheck);
        
    }
}