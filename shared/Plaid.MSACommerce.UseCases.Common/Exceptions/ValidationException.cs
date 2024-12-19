using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.MSACommerce.UseCases.Common.Exceptions
{
    /// <summary>
    /// 增加验证异常处理(没有任何验证错误信息时提示发生一个或多个验证失败)
    /// </summary>
    public class ValidationException():Exception("发生一个或多个验证失败")
    {
        /// <summary>
        /// 构造函数无参数，仅调用基类Exception的构造函数，并传递一个自定义的错误消息"发生一个或多个验证失败"
        /// 这意味着，当抛出ValidationException时，如果没有提供具体的失败信息，默认的错误消息是"发生一个或多个验证失败"
        /// </summary>
        /// <param name="failures"></param>
        public ValidationException(IEnumerable<ValidationFailure> failures):this()
        {
            //将按照属性名称分组，将错误信息存储到Errors字典集合中
            Errors = failures.GroupBy(failure => failure.PropertyName, failure => failure.ErrorMessage)
            .ToDictionary(grouping => grouping.Key, grouping => grouping.ToArray());
        }
        public IDictionary<string, string[]> Errors { get; } = new Dictionary<string, string[]>();
    }
}
