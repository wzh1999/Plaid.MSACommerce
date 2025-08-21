namespace Plaid.MSACommerce.Consul.ServiceRegistration;

/// <summary>
/// 健康检查配置类
/// </summary>
public class ServiceCheckConfiguration
{
    /// <summary>
    /// 健康检查路径
    /// </summary>
    public string Path { get; set; } = "/Health";

    /// <summary>
    /// 健康检查间隔
    /// </summary>
    public int Interval { get; set; } = 1000;

    /// <summary>
    /// 超出时间
    /// </summary>
    public int Timeout { get; set; } = 5000;

    /// <summary>
    /// 注销时间
    /// </summary>
    public int Deregister { get; set; } = 5000;
}