namespace Plaid.MSACommerce.Consul.ServiceRegistration;
/// <summary>
/// 服务配置类
/// </summary>
public class ServiceConfiguration
{
    /// <summary>
    /// 服务器名称
    /// AppDomain.CurrentDomain.FriendlyName 获取当前启动的.exe应用程序名称
    /// 那个引用它则就使用那个程序集名称使用，假设AuthService 引用了 则获取这个名称
    /// </summary>
    public string ServiceName { get; set; } = AppDomain.CurrentDomain.FriendlyName;

    /// <summary>
    /// 服务Id
    /// Guid.NewGuid().ToString() 默认创建一个GUID做为id
    /// 用于区分不同分服务实例
    /// </summary>
    public string ServiceId { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// 服务器地址
    /// 需要注册到Consul地址，也是对外提供的统一地址
    /// </summary>
    public Uri ServiceAddress { get; set; } = null!;
}