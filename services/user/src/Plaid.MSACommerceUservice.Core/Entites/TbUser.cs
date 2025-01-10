using Plaid.MSACommerce.SharedKernel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.MSACommerce.Uservice.Core.Entites
{
    /// <summary>
    /// 用户实体类-聚合根实体
    /// </summary>
    public class TbUser : BaseAuditEntity, IAggregateRoot
    {
        /// <summary>
        /// 用户名
        /// 代码中null! 表示该属性为null时编译器抑制不提示警告,它不会为空的
        /// </summary>
        public string Username { get; set; } = null!;
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = null!;
        /// <summary>
        /// 手机号
        /// </summary>
        public string? Phone { get; set; }
        /// <summary>
        /// 盐 用于用户加密的密码的盐
        /// </summary>
        public string Salt { get; set; } = null!;
    }
}
