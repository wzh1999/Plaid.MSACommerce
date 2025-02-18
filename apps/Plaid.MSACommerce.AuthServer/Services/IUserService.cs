using Refit;
namespace Plaid.MSACommerce.AuthServer.Services
{
    public record UserDto(long Id, string Username, string? Phone);
    public interface IUserService
    {
        /// <summary>
        /// 定义Refit请求的接口信息和模式,类似于httpclient操作
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [Get("/api/user")]
        Task<ApiResponse<UserDto>> GetUserAsync(string username, string password);
    }
}
