using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.MSACommerce.SharedKernel.Result
{
    /// <summary>
    /// 定义通用结果类需要用的基本属性和方法
    /// 为具体的实现类提供统一的规范
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// 错误消息
        /// 用于发生错误的时候提供相关的消息
        /// </summary>
        IEnumerable<string>? Errors { get; }
        /// <summary>
        /// 是否提交成功状态
        /// </summary>
        bool IsSuccess { get; }
        /// <summary>
        /// 结果状态
        /// 结果枚举，可以判断更加详细的结果分类 
        /// </summary>
        ResultStatus Status { get; }
        /// <summary>
        /// 
        /// </summary>
        object? GetValue();
    }
}
