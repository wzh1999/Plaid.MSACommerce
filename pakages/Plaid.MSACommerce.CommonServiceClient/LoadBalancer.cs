using System.Security.Cryptography;
using Plaid.MSACommerce.CommonServiceClient.Strategies;

namespace Plaid.MSACommerce.CommonServiceClient;
/// <summary>
/// 负载均衡器实现类
/// </summary>
/// <param name="strategy">负载均衡策略枚举</param>
public class LoadBalancer(LoadBalancingStrategy strategy) : ILoadBalancer
{
    /// <summary>
    /// 通过switch语法糖判断一下
    /// </summary>
    private readonly ILoadBalancingStrategy _strategy = strategy switch
    {
        LoadBalancingStrategy.Random => new RandomStrategy(),
        LoadBalancingStrategy.RoundRobin => new RoundRobinStrategy(),
        _ => throw new ArgumentOutOfRangeException(nameof(strategy), strategy, null)
    };

    public string GetNode(List<string> nodes)
    {
        return _strategy.Resolve(nodes);
    }
}