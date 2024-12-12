using FluentValidation;
using MediatR;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.MSACommerce.UseCases.Common.Behaviors
{
    /// <summary>
    /// 通用验证行为
    /// 使用MediatR管道来实现的命令对象或查询对象的验证
    /// where约束请求参数不可以为空
    /// </summary>
    /// <typeparam name="TRequest">请求类型</typeparam>
    /// <typeparam name="TResponse">返回类型</typeparam>
    /// <param name="validators">验证规则</param>
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        /// <summary>
        /// 用于接受请求命令进行一下处理
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <param name="next"> 是一个委托，代表处理请求的下一个处理程序。在中间件或管道行为中，你通常会在执行自己的逻辑后调用 next()，以便继续传递请求</param>
        /// <param name="cancellationToken">用于取消请求机制，允许在请求过程中取消请求</param>
        /// <returns></returns>
        /// <exception cref="ValidationException"></exception>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (validators.Any())
            {
                //基于FluentValidation库，主要用于对象模型定义验证规则， 采用Fluent API风格
                //你可以通过编写清晰、可读的代码来验证对象的属性，而不需要繁杂的手动验证或重复的 if 语句
                //验证请求参数
                var context = new ValidationContext<TRequest>(request);
                //查询所有验证规则，并异步等待验证是否成功
                var validationResults = await Task.WhenAll(validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));
                //查询验证结果中是否存在错误的
                var failures = validationResults.Where(result => result.Errors.Count != 0).SelectMany(result => result.Errors).ToList();
                //如果存在错误则直接返回
                if (failures.Count != 0)
                {
                    throw new ValidationException(failures);
                }
            }
            return await next();
        }
    }
}
