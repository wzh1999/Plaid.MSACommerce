namespace Plaid.MSACommerce.CommonServiceClient;
/// <summary>
/// 服务客户端负载均衡策略配置信息 默认是轮询
/// 用于配置客户端服务访问的一些属性
/// 主要避免注册时设置或调用的时候设置我，这样做的目的是为了更好统一入口的配置服务客户端的一些设置信息
/// </summary>
public class ServiceClientOption
{
    /// <summary>
    /// 轮询策略
    /// </summary>
    public LoadBalancingStrategy LoadBalancingStrategy { get; set; } = LoadBalancingStrategy.RoundRobin;
    /// <summary>
    /// 服务名称
    /// 用于在注册服务、配置服务的时候更好的配置加载服务的一些选项
    /// </summary>
    public string ServiceName { get; set; } = null!;
}