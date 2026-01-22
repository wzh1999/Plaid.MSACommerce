namespace Plaid.MSACommerce.CommonServiceClient.Strategies;

/// <summary>
/// 轮询负载均衡策略器服务实现类
/// </summary>
public class RoundRobinStrategy : ILoadBalancingStrategy
{
    private int _index;

    /// <summary>
    /// 解析轮询负载均衡策略器逻辑
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
        //Interlocked.Increment 确保每个线程都能按顺序访问节点，避免多个线程同时访问导致的并发问题
        _index = Interlocked.Increment(ref _index) % nodes.Count;
        return nodes[_index];
    }
}