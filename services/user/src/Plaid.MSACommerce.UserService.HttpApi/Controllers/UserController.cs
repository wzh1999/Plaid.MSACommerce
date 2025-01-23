using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plaid.MSACommerce.HttpApi.Common.Infrastructure;
using Plaid.MSACommerce.Uservice.UseCases.Commands;
using Plaid.MSACommerce.Uservice.UseCases.Queries;

namespace Plaid.MSACommerce.UserService.HttpApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ApiControllerBase
    {
        /// <summary>
        ///查询用户
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetUserQuery request)
        {
            var result = await Sender.Send(request);
            return ReturnResult(result);
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="resquer">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommand resquer)
        {
            var result = await Sender.Send(resquer);
            return ReturnResult(result);
        }
    }
}
