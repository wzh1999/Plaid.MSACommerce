using Plaid.MSACommerce.SharedKernel.Result;

namespace Plaid.MSACommerce.AuthServer.Services
{
    /// <summary>
    /// 身份认证服务
    /// </summary>
    public interface IIdentityService
    {
        /// <summary>
        /// 根据用户名称、密码获取令牌
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        Task<Result<string>> GetAccessTokenAsync(string username,string password);
    }
}
