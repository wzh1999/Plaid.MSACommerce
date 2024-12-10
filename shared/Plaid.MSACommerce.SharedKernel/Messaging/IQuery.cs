using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.MSACommerce.SharedKernel.Messaging
{
    /// <summary>
    /// 查询消息--表示CQRS查询对象
    /// </summary>
    /// <typeparam name="TResponse">返回对应的类型数据</typeparam>
    public interface IQuery<out TResponse>:IRequest<TResponse>
    {
    }
}
