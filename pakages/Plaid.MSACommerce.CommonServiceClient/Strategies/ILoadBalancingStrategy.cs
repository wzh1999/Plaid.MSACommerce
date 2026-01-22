namespace Plaid.MSACommerce.CommonServiceClient.Strategies;
/// <summary>
/// 负载均衡策略器接口
/// </summary>
public interface ILoadBalancingStrategy
{
    /// <summary>
    /// 解析策略器逻辑
    /// </summary>
    /// <param name="nodes"></param>
    /// <returns></returns>
    string Resolve(List<string> nodes);
}