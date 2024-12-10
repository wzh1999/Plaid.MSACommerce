using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.MSACommerce.SharedKernel.Messaging
{
    /// <summary>
    /// 查询处理器
    /// </summary>
    /// <typeparam name="TQuery">查询类型</typeparam>
    /// <typeparam name="TResponse">返回的结果类型</typeparam>
    public interface IQueryHandler<in TQuery,TResponse>:IRequestHandler<TQuery,TResponse>where TQuery:IQuery<TResponse>
    {
    }
}
