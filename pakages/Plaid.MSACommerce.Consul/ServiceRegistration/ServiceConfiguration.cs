namespace Plaid.MSACommerce.Consul.ServiceRegistration;

public class ServiceConfiguration
{
    /// <summary>
    /// 服务器名称
    /// AppDomain.CurrentDomain.FriendlyName 获取当前启动的.exe应用程序名称
    /// </summary>
    public string ServiceName { get; set; } = AppDomain.CurrentDomain.FriendlyName;

    /// <summary>
    /// 服务Id
    /// Guid.NewGuid().ToString() 默认创建一个GUID做为id
    /// </summary>
    public string ServiceId { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// 服务器地址
    /// </summary>
    public Uri ServiceAddress { get; set; } = null!;
}