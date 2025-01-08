using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.MSACommerce.UseCases.Common.Interfaces
{
    /// <summary>
    /// 用户名信息接口类
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        int? Id { get; }
        /// <summary>
        /// 用户名称
        /// </summary>
        string? Username { get; }
    }
}
