namespace Plaid.MSACommerce.CommonServiceClient;

/// <summary>
/// 服务客户端接口-通用客户端库
/// </summary>
public interface IServiceClient<TServiceApi> where TServiceApi : class
{
    /// <summary>
    /// 服务名称
    /// </summary>
    string ServiceName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    TServiceApi ServiceApi { get; set; }
}