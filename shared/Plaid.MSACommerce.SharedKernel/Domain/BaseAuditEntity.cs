using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.MSACommerce.SharedKernel.Domain
{
    /// <summary>
    /// 审计时间属性抽象基类-基础审计实体
    /// </summary>
    public abstract class BaseAuditEntity:BaseEntity<long>
    {
        /// <summary>
        /// 创建时间-DateTimeOffset带有时区和时间偏差，UTC时间
        /// </summary>
        public DateTimeOffset? CreatedAt { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTimeOffset? LastModifiedAt { get; set; }
    }
}
