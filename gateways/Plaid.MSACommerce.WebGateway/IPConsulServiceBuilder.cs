using Consul;
using Ocelot.Logging;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Consul.Interfaces;

namespace Plaid.MSACommerce.WebGateway;

/// <summary>
/// 服务发现自定义 服务发现构建器 用于本地开发 避免拿到是主机名导致无法获取到对应的服务
/// 注：base中configurationFactory  需要安装ocelot 24以下的版本不然传的是IHttpContextAccessor 不是委托
/// </summary>
public class IPConsulServiceBuilder : DefaultConsulServiceBuilder
{
    public IPConsulServiceBuilder(Func<ConsulRegistryConfiguration> configurationFactory,
        IConsulClientFactory clientFactory, IOcelotLoggerFactory loggerFactory) : base(configurationFactory,
        clientFactory, loggerFactory)
    {
    }

    /// <summary>
    /// 获取下游主机 调整成ip地址 默认是返回主机名
    /// </summary>
    /// <param name="entry"></param>
    /// <param name="node"></param>
    /// <returns></returns>
    protected override string GetDownstreamHost(ServiceEntry entry, Node node) => entry.Service.Address;
}