using System.ComponentModel.Design;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Plaid.MSACommerce.UseCases.Common;

namespace Plaid.MSACommerceUservice.UseCases
{
    public static class DependencyInjection
    {
        /// <summary>
        /// 定义扩展服务注册扩展方法
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddUseCase(this IServiceCollection services)
        {
            services.AddUseCaseCommon(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
