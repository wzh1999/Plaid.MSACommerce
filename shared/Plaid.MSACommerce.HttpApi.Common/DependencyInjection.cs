using Microsoft.Extensions.DependencyInjection;
using Plaid.MSACommerce.HttpApi.Common.Infrastructure;
using Plaid.MSACommerce.HttpApi.Common.Services;
using Plaid.MSACommerce.UseCases.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.MSACommerce.HttpApi.Common
{
    public static class DependencyInjection
    {
        /// <summary>
        /// 定义扩展方法注册服务入口
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddHttpApiCommon(this IServiceCollection services)
        {
            services.AddScoped<IUser, CurrentUser>();
            services.AddHttpContextAccessor();
            services.AddExceptionHandler<UseCaseExceptionHandler>();
            services.AddProblemDetails();
            ConfigureCors(services);
            return services;
        }

        /// <summary>
        /// 解决跨域问题
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAny", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
        }
    }
}
