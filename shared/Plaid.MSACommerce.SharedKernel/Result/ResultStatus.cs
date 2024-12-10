using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.MSACommerce.SharedKernel.Result
{
    /// <summary>
    /// 返回结果状态枚举
    /// </summary>
    public enum ResultStatus
    {
        /// <summary>
        /// 成功
        /// </summary>
        Ok,
        /// <summary>
        /// 错误
        /// </summary>
        Error,
        /// <summary>
        /// 拒绝访问
        /// </summary>
        Forbidden,
        /// <summary>
        /// 未认证
        /// </summary>
        Unauthorized,
        /// <summary>
        /// 未找到
        /// </summary>
        NotFound,
        /// <summary>
        /// 操作无效
        /// </summary>
        Invalid
    }
}
