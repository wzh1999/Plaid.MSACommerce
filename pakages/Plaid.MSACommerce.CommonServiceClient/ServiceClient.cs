using Plaid.MSACommerce.Consul.ServiceDiscovery;

namespace Plaid.MSACommerce.CommonServiceClient;
/// <summary>
/// 服务客户端实现类 继承自服务客户端接口
/// </summary>
public abstract class ServiceClient : IServiceClient
{
    /// <summary>
    /// 服务名称
    /// </summary>
    public virtual string ServiceName { get; set; }

    /// <summary>
    /// 构造函数注入
    /// </summary>
    /// <param name="serviceDiscpvery">注入服务发现</param>
    /// <param name="loadBalancer">注入负载均衡器</param>
    /// <param name="httpClient">注入httpClient</param>
    protected ServiceClient(IServiceDiscpvery serviceDiscpvery, ILoadBalancer loadBalancer, HttpClient httpClient)
    {
        //通过服务发现 传入服务名称获取服务列表包括服务地址和端口
        var serviceList = serviceDiscpvery.GetServicesAsync(ServiceName).Result;
        //根据服务地址和端口结合负载均衡器获取服务节点
        var serviceAddress = loadBalancer.GetNode(serviceList);
        //指定访问地址是什么
        httpClient.BaseAddress = new Uri($"http://{serviceAddress}");
    }
}