using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.MSACommerce.SharedKernel.Domain
{
    public abstract class BaseEvent:INotification
    {
        /// <summary>
        /// DateTimeOffset相比于DateTime多了多时区和时间偏移
        /// 用于记录领域事件操作时间
        /// </summary>
        public DateTimeOffset DateOccurred { get; protected set; } = DateTimeOffset.Now;
    }
}
