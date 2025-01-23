using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Plaid.MSACommerce.UseCases.Common.Behaviors;

namespace Plaid.MSACommerce.UseCases.Common
{
    public static class DependencyInjection
    {
        /// <summary>
        /// 定义扩展方法注册服务入口
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IServiceCollection AddUseCaseCommon(this IServiceCollection services, Assembly assembly) {
            //注册对象映射帮助类
            services.AddAutoMapper(assembly);
            //注册FluentValidation验证器 它是一个验证框架
            services.AddValidatorsFromAssembly(assembly);
            //注册MediatR中介者服务及中间件
            services.AddMediatR(cfg =>
            {
                //将通过扫描的Assembly文件中的所有的程序集注册到MediatR中命令器(如请求、查询、处理器等)
                //根据它们是否实现了上述接口（如 IRequest、IRequestHandler、INotification 等)来进行扫描的,当然也可以采用构造函数的方式注入
                cfg.RegisterServicesFromAssembly(assembly);
                //实现IPipelineBehavior<,>当MediatR请求处理流程中都会去执行ValidationBehavior进行验证
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });
            return services;
        }
    }
}
