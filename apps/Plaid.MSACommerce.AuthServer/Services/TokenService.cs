using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Plaid.MSACommerce.AuthServer.Services;

/// <summary>
/// 令牌服务接口 颁发 刷新 授权令牌服务
/// </summary>
/// <param name="jwtSettings"></param>
public class TokenService(IOptions<JwtSettings> jwtSettings) : ITokenService
{
    /// <summary>
    /// 生成访问令牌
    /// </summary>
    /// <param name="claims">自定义token中信息</param>
    /// <returns></returns>
    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Value.Secret));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var jwt = new JwtSecurityToken(
            jwtSettings.Value.Issuer,
            jwtSettings.Value.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(jwtSettings.Value.AccessTokenExpirationMinutes),
            signingCredentials: signinCredentials);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(jwt);
        return tokenString;
    }
    /// <summary>
    /// 生成刷新令牌
    /// </summary>
    /// <returns></returns>
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    /// <summary>
    /// 过期令牌中获取用户详细信息
    /// </summary>
    /// <param name="token">过期token</param>
    /// <returns></returns>
    /// <exception cref="SecurityTokenException"></exception>
    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false, //过期时间不需要验证，因为是从过期令牌中获取用户
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Value.Issuer,
            ValidAudience = jwtSettings.Value.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.Value.Secret)
            )
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Token无效");
        }

        return principal;
    }
}