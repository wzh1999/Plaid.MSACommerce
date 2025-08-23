using Consul;
using Consul.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Plaid.MSACommerce.Consul.ServiceRegistration;

/// <summary>
/// 服务注册的扩展类
/// </summary>
public static class ConsulServiceRegistrationExtensions
{
    /// <summary>
    /// 服务注册配置方法
    /// </summary>
    /// <param name="services">给 IServiceCollection 添加功能。</param>
    /// <param name="serviceConfigure">调用者通过 Lambda 回调来配置服务信息（名字、地址等）</param>
    /// <param name="serviceCheckConfiguration">健康检查配置（可选，不传就用默认值）</param>
    /// <returns></returns>
    public static IServiceCollection AddConsulService(this IServiceCollection services,
        Action<ServiceConfiguration> serviceConfigure,
        ServiceCheckConfiguration? serviceCheckConfiguration)
    {
        //实例化创建服务配置类
        var serviceConfiguration = new ServiceConfiguration();
        //通过将服务类实例化好后作为参数交给Action委托，可以让调用方进行回调对这个配置类中的属性进行修改，然后再又框架吧这个改完的实例拿去继续初始化
        //核心是通过一个匿名委托的方式提供一个lamdba表达式的匿名方法，可以让你进行调整和修改
        serviceConfigure.Invoke(serviceConfiguration);

        serviceCheckConfiguration ??= new ServiceCheckConfiguration();

        //UriBuilder用来拼装、修改、解析URL的安全方便可读写的工具类
        //健康检查地址拼接
        //ServiceAddress=http://localhost:500 Path=/health 则最终为 http://localhost:500/health
        var healthCheckUri = new UriBuilder(serviceConfiguration.ServiceAddress)
        {
            Path = serviceCheckConfiguration.Path
        };
        
        //注册到Consul
        services.AddConsulServiceRegistration(serviceRegistration =>
        {
            //Consul唯一标识
            serviceRegistration.ID = serviceConfiguration.ServiceId;
            //服务名称
            serviceRegistration.Name = serviceConfiguration.ServiceName;
            //服务名或ip
            serviceRegistration.Address = serviceConfiguration.ServiceAddress.Host;
            //服务监听端口号
            serviceRegistration.Port = serviceConfiguration.ServiceAddress.Port;
            serviceRegistration.Check = new AgentServiceCheck
            {
                //定期访问的健康检查URL
                HTTP = healthCheckUri.ToString(),
                //多久检查一次 30s
                Interval = TimeSpan.FromMilliseconds(serviceCheckConfiguration.Interval),
                //单次检查超时时间 5s
                Timeout = TimeSpan.FromMilliseconds(serviceCheckConfiguration.Timeout),
                //连续失败后多久自动注销 60s
                DeregisterCriticalServiceAfter = TimeSpan.FromMilliseconds(serviceCheckConfiguration.Deregister),
            };
        });
        return services;
    }
}