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
        protected ISender Sender => HttpContext.RequestServices.GetRequiredService<ISender>();

        [NonAction]
        public IActionResult ReturnResult(IResult result)
        {
            switch (result.Status)
            {
                case ResultStatus.Ok:
                    {
                        var value = result.GetValue();
                        return value is null ? NoContent() : Ok(value);
                    }
                case ResultStatus.Error:
                    return result.Errors is null ? BadRequest() : BadRequest(new { errors = result.Errors });
                case ResultStatus.NotFound:
                    return result.Errors is null ? BadRequest() : NotFound(new { errors = result.Errors });
                case ResultStatus.Invalid:
                    return result.Errors is null ? BadRequest() : BadRequest(new { errors = result.Errors });
                case ResultStatus.Forbidden:
                    return StatusCode(403);
                case ResultStatus.Unauthorized:
                    return Unauthorized();
                default:
                    return BadRequest(new { errors = result.Errors });
            }
        }
    }
}
