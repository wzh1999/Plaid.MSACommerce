using System.Security.Claims;

namespace Plaid.MSACommerce.AuthServer.Services;
/// <summary>
/// 令牌服务接口 颁发 刷新 授权令牌接口
/// </summary>
public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}