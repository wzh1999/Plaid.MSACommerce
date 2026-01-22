namespace Plaid.MSACommerce.Consul.ServiceDiscovery;
/// <summary>
/// 服务发现接口
/// </summary>
public interface IServiceDiscpvery
{
    /// <summary>
    /// 获取指定服务名称的所有实例地址
    /// </summary>
    /// <param name="serviceName">服务名称</param>
    /// <returns></returns>
    Task<List<string>> GetServicesAsync(string serviceName);
}