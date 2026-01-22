namespace Plaid.MSACommerce.CommonServiceClient;
/// <summary>
/// 负载均衡器接口
/// </summary>
public interface ILoadBalancer
{
    /// <summary>
    /// 根据节点列表获取一个节点
    /// </summary>
    /// <param name="nodes">服务列表</param>
    /// <returns>选中的节点</returns>
    string GetNode(List<string> nodes);
}