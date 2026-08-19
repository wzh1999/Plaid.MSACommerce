using System.Security.Claims;

namespace Plaid.MSACommerce.AuthServer.Services;

/// <summary>
/// 令牌服务接口 颁发 刷新 授权令牌接口
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// 获取认证token
    /// </summary>
    /// <param name="claims">自定义用户信息</param>
    /// <returns></returns>
    string GenerateAccessToken(IEnumerable<Claim> claims);

    /// <summary>
    /// 获取刷新令牌
    /// </summary>
    /// <returns></returns>
    string GenerateRefreshToken();

    /// <summary>
    /// 根据过期token获取用户详细信息
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}