using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.MSACommerce.SharedKernel.Messaging
{
    /// <summary>
    /// 命令处理器--都来继承自MeditaR  
    /// </summary>
    /// <typeparam name="TCommand">发送命令</typeparam>
    /// <typeparam name="TResponse">返回的响应</typeparam>
    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>where TCommand:ICommand<TResponse>
    {
    }
}
