using Plaid.MSACommerce.Consul.ServiceDiscovery;

namespace Plaid.MSACommerce.CommonServiceClient;

public abstract class ServiceClient : IServiceClient
{
    public virtual string ServiceName { get; set; }

    /// <summary>
    /// 构造函数注入
    /// </summary>
    /// <param name="serviceDiscpvery"></param>
    /// <param name="loadBalancer"></param>
    /// <param name="httpClient"></param>
    protected ServiceClient(IServiceDiscpvery serviceDiscpvery, ILoadBalancer loadBalancer, HttpClient httpClient)
    {
        var serviceList = serviceDiscpvery.GetServicesAsync(ServiceName).Result;
        var serviceAddress = loadBalancer.GetNode(serviceList);
        httpClient.BaseAddress = new Uri($"http://{serviceAddress}");
    }
}