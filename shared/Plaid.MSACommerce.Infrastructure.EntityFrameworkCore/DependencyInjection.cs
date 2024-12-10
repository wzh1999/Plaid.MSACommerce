using Microsoft.Extensions.DependencyInjection;
using Plaid.MSACommerce.Infrastructure.EntityFrameworkCore.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.MSACommerce.Infrastructure.EntityFrameworkCore
{
    /// <summary>
    /// 扩展方法--用于注入服务
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureCommon(this IServiceCollection services) {

            //依赖注入审计实体拦截类
            services.AddScoped<AuditEntityInterceptor>();
            return services;
        }
    }
}
