namespace Plaid.MSACommerce.Consul.ServiceDiscovery;

public interface IServiceDiscpvery
{
    Task<List<string>> GetServicesAsync(string serviceName);
}