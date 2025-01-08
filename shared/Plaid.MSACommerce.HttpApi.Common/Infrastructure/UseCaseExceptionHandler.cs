using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plaid.MSACommerce.UseCases.Common.Exceptions;

namespace Plaid.MSACommerce.HttpApi.Common.Infrastructure
{
    /// <summary>
    /// 用例异常处理器
    /// 主要作用于处理不同类型的异常并生成相应的HTTP响应，目的是捕获特定类型的异常，并将它们转化为适当的HTTP错误响应，继承自IExceptionHandler
    /// </summary>
    public class UseCaseExceptionHandler : IExceptionHandler
    {
        /// <summary>
        /// 创建一个字典，将ValidationException、ForbiddenException错误类型映射到HandlerValidationException、HandleForbiddenExceptionAsync方法进行处理
        /// 可以根据异常类型，选择一个对应的处理方法
        /// </summary>
        private readonly Dictionary<Type, Func<HttpContext, Exception, Task>> _exceptionHandlers = new()
        {
            { typeof(ValidationException),HandlerValidationException},
            { typeof(ForbiddenException),HandleForbiddenExceptionAsync}
        };
        /// <summary>
        /// 用于处理异常信息
        /// </summary>
        /// <param name="httpContext">获取请求上下文信息</param>
        /// <param name="exception"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            //获取异常类型
            var exceptionType = exception.GetType();
            //判断异常类型是否在字典中
            if (!_exceptionHandlers.TryGetValue(exceptionType, out var handler))
            {
                return false;
            }
            //如果存在则执行这个委托方法
            await handler.Invoke(httpContext, exception);
            return true;
        }
        /// <summary>
        /// ValidationException类型的异常处理方法
        /// 验证错误异常
        /// </summary>
        /// <param name="httpContent">http请求上下文</param>
        /// <param name="exception">异常类型</param>
        /// <returns></returns>
        private static async Task HandlerValidationException(HttpContext httpContent, Exception exception)
        {
            //将异常类型转换为ValidationException
            var validationException = (ValidationException)exception;
            //将状态码赋值到http上下文中
            httpContent.Response.StatusCode = StatusCodes.Status400BadRequest;
            //type这个地址表示描述状态码400的标准描述
            await httpContent.Response.WriteAsJsonAsync(new ValidationProblemDetails(validationException.Errors)
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "https://developer.mozilla.org/zh-CN/docs/Web/HTTP/Status/400"
            });
        }
        /// <summary>
        /// ForbiddenException类型的异常处理方法
        /// 服务器理解请求，并拒绝访问
        /// </summary>
        /// <param name="httpContext">http请求上下文</param>
        /// <param name="exception">异常处理类型</param>
        /// <returns></returns>
        private static async Task HandleForbiddenExceptionAsync(HttpContext httpContext, Exception exception)
        {

            httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = StatusCodes.Status403Forbidden,
                Title = "Forbidden",
                Type = "https://developer.mozilla.org/zh-CN/docs/Web/HTTP/Status/403"
            });
        }
    }
}
