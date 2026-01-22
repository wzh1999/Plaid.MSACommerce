using Consul;

namespace Plaid.MSACommerce.Consul.ServiceDiscovery;
/// <summary>
/// 实现基于Consul的服务发现
/// </summary>
/// <param name="consulClient">Consul客户端,在Nuget包引入的时候会自动注册</param>
public class ConsulServiceDiscovery(IConsulClient consulClient) : IServiceDiscpvery
{
    /// <summary>
    /// 获取指定服务名称的所有实例地址
    /// </summary>
    /// <param name="serviceName">服务名称</param>
    /// <returns></returns>
    public async Task<List<string>> GetServicesAsync(string serviceName)
    {
        //根据Counsul服务上下文通过健康节点查询服务实例
        var queryResult = await consulClient.Health.Service(serviceName, null, true);
        //从查询结果中提取服务实例的地址和端口 并返回
        return queryResult.Response
            .Select(serviceEntry => serviceEntry.Service.Address + ":" + serviceEntry.Service.Port).ToList();
    }
}