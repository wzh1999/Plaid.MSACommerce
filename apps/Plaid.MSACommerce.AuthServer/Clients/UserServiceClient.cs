using Plaid.MSACommerce.AuthServer.Services;
using Plaid.MSACommerce.CommonServiceClient;
using Plaid.MSACommerce.CommonServiceClient.AspNetCore;
using Plaid.MSACommerce.Consul.ServiceDiscovery;
using Refit;

namespace Plaid.MSACommerce.AuthServer.Clients;
/// <summary>
/// 构造函数注入
/// </summary>
/// <param name="serviceDiscpvery">注入服务发现</param>
/// <param name="loadBalancer">注入负载均衡策略器</param>
/// <param name="httpClient">注入http请求</param>
public class UserServiceClient(IServiceDiscpvery serviceDiscpvery,
    ILoadBalancer<UserServiceClient> loadBalancer,
    HttpClient httpClient):ServiceClient(serviceDiscpvery,loadBalancer,httpClient)
{
    public override string ServiceName { get; set; }="Plaid.MSACommerce.UserService.HttpApi";
    public readonly IUserService UserServiceApi=RestService.For<IUserService>(httpClient);
}