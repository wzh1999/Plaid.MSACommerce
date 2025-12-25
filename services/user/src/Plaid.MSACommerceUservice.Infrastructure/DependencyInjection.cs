using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Plaid.MSACommerce.Infrastructure.EntityFrameworkCore;
using Plaid.MSACommerce.Infrastructure.EntityFrameworkCore.Interceptors;
using Plaid.MSACommerce.Uservice.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plaid.MSACommerce.Infrastructure.Common;

namespace Plaid.MSACommerce.Uservice.Infrastructure
{
    /// <summary>
    /// .NET配置扩展方法
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// 服务配置扩展方法
        /// </summary>
        /// <param name="services">服务配置上下文</param>
        /// <param name="configuration">读取环境配置上下文</param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //加载Consul中的配置信息
            services.AddInfrastructureCommon(configuration);
            //加载公共服务中的ef中的扩展方法配置信息
            services.AddInfrastructureEfCore();
            ConfigureEfCore(services, configuration);
            return services;
        }

        /// <summary>
        /// 配置数据库链接,数据库上下文注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        private static void ConfigureEfCore(IServiceCollection services, IConfiguration configuration)
        {

            var dbConnection = configuration.GetConnectionString("UserDbConnection");
            //配置UserDbContext
            //AddDbContext通过依赖关系注入隐式共享 DbContext 实例
            //可以使用 DbContextOptionsBuilder 创建 DbContextOptions 对象，然后将该对象传递到 DbContext 构造函数。 这使得为依赖关系注入配置的 DbContext 也能显式构造
            services.AddDbContext<UserDbContext>((sp, options) =>
            {
                //添加拦截器
                //AddInterceptors 方法把自定义的 AuditEntityInterceptor 拦截器添加到 DbContext，让它在数据库操作（如 SaveChanges）时起作用。
                //通过这种方式，可以在所有数据库操作中实现统一的审计逻辑，而无需在每个操作中手动添加审计代码。
                options.AddInterceptors(sp.GetRequiredService<AuditEntityInterceptor>());
                //通过 AddDbContext 将其注册到依赖注入容器中，使用配置中的连接字符串连接到 MySQL 数据库
                options.UseMySql(dbConnection, ServerVersion.AutoDetect(dbConnection));
            });
        }
    }
}
