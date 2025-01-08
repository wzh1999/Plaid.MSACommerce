using Microsoft.AspNetCore.Http;
using Plaid.MSACommerce.UseCases.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.MSACommerce.HttpApi.Common.Services
{
    /// <summary>
    /// 用于提取用户信息
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    public class CurrentUser(IHttpContextAccessor httpContextAccessor) : IUser
    {
        /// <summary>
        /// 获取Http请求中的用户信息，从JWT中提取
        /// </summary>
        private readonly ClaimsPrincipal? _user = httpContextAccessor.HttpContext?.User;
        //获取用户id
        public int? Id
        {
            get
            {
                var id = _user?.FindFirstValue(ClaimTypes.NameIdentifier);
                if (id is null) return null;
                return Convert.ToInt32(id);
            }
        }
        //获取用户名称
        public string? Username => _user?.FindFirstValue(ClaimTypes.Name);
    }
}
