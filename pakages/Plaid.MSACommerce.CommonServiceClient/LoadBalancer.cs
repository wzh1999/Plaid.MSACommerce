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
    /// 通过switch语法糖判断一下 当前策略方式
    /// </summary>
    private readonly ILoadBalancingStrategy _strategy = strategy switch
    {
        //随机策略 实例化随机策略对象
        LoadBalancingStrategy.Random => new RandomStrategy(),
        //轮询策略 实例化轮询策略对象
        LoadBalancingStrategy.RoundRobin => new RoundRobinStrategy(),
        //兜底 如果没有任何策略 则抛出无效操作异常
        _ => throw new ArgumentOutOfRangeException(nameof(strategy), strategy, null)
    };
    
    /// <summary>
    /// 获取节点
    /// </summary>
    /// <param name="nodes"></param>
    /// <returns></returns>
    public string GetNode(List<string> nodes)
    {
        //通过策略器进行解析
        return _strategy.Resolve(nodes);
    }
}