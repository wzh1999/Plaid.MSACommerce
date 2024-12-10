using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.MSACommerce.SharedKernel.Domain
{
    /// <summary>
    /// 审计用户属性抽象
    /// </summary>
    public abstract class AuditwithUserEntity:BaseAuditEntity
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public int? CreateBy { get; set; }
        /// <summary>
        /// 最后修改人
        /// </summary>
        public int? LastModifiedBy { get; set; }
    }
}
