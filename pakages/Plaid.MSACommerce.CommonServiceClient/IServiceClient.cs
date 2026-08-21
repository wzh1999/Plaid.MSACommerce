namespace Plaid.MSACommerce.CommonServiceClient;

/// <summary>
/// 服务客户端接口
/// </summary>
public interface IServiceClient<TServiceApi> where TServiceApi : class
{
    /// <summary>
    /// 服务名称
    /// </summary>
    string ServiceName { get; set; }

    TServiceApi ServiceApi { get; set; }
}