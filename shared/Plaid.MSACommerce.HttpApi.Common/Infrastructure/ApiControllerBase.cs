using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Plaid.MSACommerce.SharedKernel.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IResult = Plaid.MSACommerce.SharedKernel.Result.IResult;


namespace Plaid.MSACommerce.HttpApi.Common.Infrastructure
{
    /// <summary>
    /// 扩展基础控制器
    /// </summary>
    public abstract class ApiControllerBase : ControllerBase
    {
        /// <summary>
        /// 注入Sender实例，基于MediatR库，用于消息的发送(如命令、查询)
        /// 通过查询HttpContext.RequestServices.GetRequiredServic请求上下文依赖注入容器（DI）获取Send对象,这样只要继承自它就可以直接使用MediatR实例,当然也可以继续使用依赖构造函数注入
        /// </summary>
        protected ISender Sender => HttpContext.RequestServices.GetRequiredService<ISender>();

        /// <summary>
        /// 处理不同的Htpp响应
        /// </summary>
        /// <param name="result">获取响应的状态</param>
        /// <returns></returns>
        [NonAction]
        public IActionResult ReturnResult(IResult result)
        {
            switch (result.Status)
            {
                //状态Ok
                case ResultStatus.Ok:
                    {
                        //获取值
                        var value = result.GetValue();
                        //如果值为空返回204状态表示没有返回内容，正常则返回200并附带返回内容
                        return value is null ? NoContent() : Ok(value);
                    }
                //状态为错误信息，则返回400并附带错误信息，如果没有错误信息则直接返回空
                case ResultStatus.Error:
                    return result.Errors is null ? BadRequest() : BadRequest(new { errors = result.Errors });
                //未找到
                case ResultStatus.NotFound:
                    return result.Errors is null ? BadRequest() : NotFound(new { errors = result.Errors });
                //无效
                case ResultStatus.Invalid:
                    return result.Errors is null ? BadRequest() : BadRequest(new { errors = result.Errors });
                //禁止访问
                case ResultStatus.Forbidden:
                    return StatusCode(403);
                //未授权
                case ResultStatus.Unauthorized:
                    return Unauthorized();
                default:
                    return BadRequest(new { errors = result.Errors });
            }
        }
    }
}
