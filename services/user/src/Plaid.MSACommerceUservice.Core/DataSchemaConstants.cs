using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.MSACommerce.Uservice.Core
{
    /// <summary>
    /// 数据架构的一些约定
    /// </summary>
    public class DataSchemaConstants
    {
        /// <summary>
        /// 用户名最大长度
        /// </summary>
        public const int DafeultUsernameMaxLength = 32;
        /// <summary>
        /// 密码最大长度
        /// </summary>
        public const int DafeultPasswordMinLength = 6;

        /// <summary>
        /// 密码最大长度
        /// </summary>
        public const int DafeultPasswordMaxLength = 128;
        /// <summary>
        /// 手机号最大长度
        /// </summary>
        public const int DafeultPhoneMaxLength = 11;
        /// <summary>
        /// 加盐最大长度
        /// </summary>
        public const int DafeultSaltMaxLength = 32;

    }
}
