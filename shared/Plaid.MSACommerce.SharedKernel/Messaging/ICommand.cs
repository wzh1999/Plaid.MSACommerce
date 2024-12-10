using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.MSACommerce.SharedKernel.Messaging
{
    /// <summary>
    /// 命令消息--命令对象 
    /// IRequest<TResponse>表示该命令返回的结果为
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
