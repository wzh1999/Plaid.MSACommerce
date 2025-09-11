using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Plaid.MSACommerce.Consul.ServiceRegistration;

namespace Plaid.MSACommerce.HttpApi.Common;

/// <summary>
/// 微服务通用服务配置中间件入口
/// </summary>
public static class AppBuilderExtensions
{
    /// <summary>
    /// 通用中间件注入
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseHttpCommon(this IApplicationBuilder app)
    {
        //从IOptions获取到配置集合，获取对应属性值
        var serviceCheck = app.ApplicationServices.GetRequiredService<IOptions<ServiceCheckConfiguration>>().Value;
       //配置健康检查地址
        app.UseHealthChecks(serviceCheck.Path);
       //认证
        app.UseAuthentication();
       //授权
        app.UseAuthorization();
       //异常处理
        app.UseExceptionHandler(_ => { });
        return app;
    }
}