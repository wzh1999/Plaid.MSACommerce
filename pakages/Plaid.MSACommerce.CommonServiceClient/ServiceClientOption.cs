namespace Plaid.MSACommerce.CommonServiceClient;
/// <summary>
/// 服务客户端负载均衡策略配置信息 默认是轮询
/// </summary>
public class ServiceClientOption
{
    /// <summary>
    /// 轮询策略
    /// </summary>
    public LoadBalancingStrategy LoadBalancingStrategy { get; set; } = LoadBalancingStrategy.RoundRobin;
    /// <summary>
    /// 服务名称
    /// </summary>
    public string ServiceName { get; set; } = null!;
}