using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plaid.MSACommerce.AuthServer.Apis;
using Plaid.MSACommerce.AuthServer.Services;
using Plaid.MSACommerce.CommonServiceClient;
using StackExchange.Redis;

namespace Plaid.MSACommerce.AuthServer.Controllers
{
    [Route("api/token")]
    [ApiController]
    // public class TokenController(IIdentityService identityService) : ControllerBase
    public class TokenController(
        ITokenService tokenService,
        IServiceClient<IUserServiceApi> client,
        IConnectionMultiplexer redis) : ControllerBase
    {
        /// <summary>
        /// redis上下文服务
        /// </summary>
        private readonly IDatabase _redisDb = redis.GetDatabase();

        /// <summary>
        /// 获取认证token 刷新token
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(string username, string password)
        {
            #region 没有加入刷新令牌

            // var result = await identityService.GetAccessTokenAsync(username, password);
            // return result.IsSuccess ? Ok(result.Value) : BadRequest(new { errors = result.Errors });

            #endregion

            //验证用户名和密码
            var response = await client.ServiceApi.GetUserAsync(username, password);
            if (!response.IsSuccessStatusCode)
            {
                return Unauthorized();
            }

            var user = response.Content;

            //生成用户声明
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.Username),
                new(ClaimTypes.MobilePhone, user.Phone ?? ""),
            };
            var accessToken = tokenService.GenerateAccessToken(claims);
            var refreshToken = tokenService.GenerateRefreshToken();

            //保存刷新令牌-redis string类型存储
            var key = $"user:refreshToken:{username}";

            await _redisDb.StringSetAsync(key, accessToken, TimeSpan.FromDays(7), When.Always);
            return Ok(new
            {
                AccessToken = accessToken,
                refreshToken = refreshToken
            });
        }

        /// <summary>
        /// 刷新token获取新的 token 和刷新token
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        [HttpPut("refresh")]
        public async Task<IActionResult> Refresh(string accessToken, string refreshToken)
        {
            var principal = tokenService.GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity.Name;
            // 需不需要二次鉴权...
            // 查数据库……

            //检查刷新令牌是否过期
            var key = $"user:refreshToken:{username}";
            var dbRefreshToken = await _redisDb.StringGetAsync(key);
            if (dbRefreshToken.IsNull || dbRefreshToken != refreshToken)
            {
                return BadRequest("无效的请求");
            }

            var newAccessToken = tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = tokenService.GenerateRefreshToken();
            //更新redis中的值 keepTtl为true表示不更新过期时间 避免整个还原 
            await _redisDb.StringSetAsync(key, newRefreshToken, keepTtl: true);

            return Ok(new
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        /// <summary>
        /// 删除刷新token
        /// </summary>
        /// <returns></returns>
        [HttpDelete,Authorize]
        public async Task<IActionResult> Revoke()
        {
            var username = User.Identity.Name;
            var key = $"user:refreshToken:{username}";
            await _redisDb.KeyDeleteAsync(key);
            return NoContent();
        }
    }
}