using Plaid.MSACommerce.SharedKernel.Result;

namespace Plaid.MSACommerce.AuthServer.Services
{
    public interface IIdentityService
    {
        Task<Result<string>> GetAccessTokenAsync(string username,string password);
    }
}
