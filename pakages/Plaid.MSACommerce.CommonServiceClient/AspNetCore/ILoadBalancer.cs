namespace Plaid.MSACommerce.CommonServiceClient.AspNetCore;
/// <summary>
/// 负载均衡器泛型接口
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ILoadBalancer<T> : ILoadBalancer where T : class;