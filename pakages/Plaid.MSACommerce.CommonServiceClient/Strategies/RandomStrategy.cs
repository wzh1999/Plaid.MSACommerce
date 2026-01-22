namespace Plaid.MSACommerce.CommonServiceClient.Strategies;

/// <summary>
/// 随机负载均衡策略器服务实现类
/// </summary>
public class RandomStrategy : ILoadBalancingStrategy
{
    private readonly Random _random = new();

    /// <summary>
    /// 解析随机负载均衡策略器逻辑
    /// </summary>
    /// <param name="nodes"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public string Resolve(List<string> nodes)
    {
        if (nodes.Count == 0)
        {
            throw new InvalidOperationException("无可用节点");
        }

        var index = _random.Next(nodes.Count);
        return nodes[index];
    }
}