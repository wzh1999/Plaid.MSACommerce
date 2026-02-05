namespace Plaid.MSACommerce.CommonServiceClient;
/// <summary>
/// 服务客户端接口
/// </summary>
public interface IServiceClient
{
    /// <summary>
    /// 服务名称
    /// </summary>
    string ServiceName { get; set; }
}