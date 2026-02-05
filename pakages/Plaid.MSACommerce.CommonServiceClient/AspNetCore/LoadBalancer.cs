namespace Plaid.MSACommerce.CommonServiceClient.AspNetCore;

/// <summary>
/// 负载均衡器扩展出来的泛型方法
/// </summary>
/// <param name="strategy"></param>
/// <typeparam name="T"></typeparam>
public class LoadBalancer<T>(LoadBalancingStrategy strategy) : LoadBalancer(strategy), ILoadBalancer<T> where T : class;