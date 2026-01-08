using Consul;

namespace Plaid.MSACommerce.Consul.ServiceDiscovery;

public class ConsulServiceDiscovery(IConsulClient consulClient) : IServiceDiscpvery
{
    public async Task<List<string>> GetServicesAsync(string serviceName)
    {
        var queryResult = await consulClient.Health.Service(serviceName, null, true);
        return queryResult.Response
            .Select(serviceEntry => serviceEntry.Service.Address + ":" + serviceEntry.Service.Port).ToList();
    }
}