using System.Runtime.CompilerServices;
using Plaid.MSACommerce.HttpApi.Common;

namespace Plaid.MSACommerce.UserService.HttpApi
{
    public static class DependencyInjection
    {
        /// <summary>
        ///  定义扩展方法注册服务入口
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddHttpApi(this IServiceCollection services)
        {
            services.AddHttpApiCommon();
            return services;
        }
    }
}
