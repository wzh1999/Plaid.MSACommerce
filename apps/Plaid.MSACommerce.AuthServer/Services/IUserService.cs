using Refit;
namespace Plaid.MSACommerce.AuthServer.Services
{
    public record UserDto(long Id, string Username, string? Phone);
    public interface IUserService
    {
        /// <summary>
        /// 定义Refit请求的接口信息和模式,类似于httpclient操作
        /// 根据Get标记的路由进行请求不会将方法名称做为最后一层 http地址/api/user?usernam=xxx&password=xxx会命中user控制器中的get方法并接受这两个参数的api接口
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [Get("/api/user")]
        Task<ApiResponse<UserDto>> GetUserAsync(string username, string password);
    }
}
