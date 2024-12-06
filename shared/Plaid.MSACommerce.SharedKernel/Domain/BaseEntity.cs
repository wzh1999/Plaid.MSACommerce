using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.MSACommerce.SharedKernel.Domain
{
    /// <summary>
    /// 实体基础接口
    /// </summary>
    /// <typeparam name="TId">主键Id</typeparam>
    public abstract class BaseEntity<TId> : IEntity<TId>
    {
        /// <summary>
        /// 领域事件操作集合
        /// </summary>
        private readonly List<BaseEvent> _domainEvents = [];

        /// <summary>
        /// 用于存储领域事件，但是该字段不会映射到实体中
        /// AsReadOnly()表示只读，防止外部进行修改
        /// </summary>
        [NotMapped]
        public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();
        /// <summary>
        /// 主键Id default!用来在获取默认值的同时告诉编译器该值不会为 null通常用于非空引用类型的情况。
        /// </summary>
        public TId Id { get; set; } = default!;

        /// <summary>
        /// 提交领域事件
        /// </summary>
        /// <param name="domainEvent">领域事件</param>
        public void AddDomainEvent(BaseEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        /// <summary>
        /// 删除领域事件
        /// </summary>
        /// <param name="domainEvent">领域事件</param>
        public void RemoveDomainEvent(BaseEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        /// <summary>
        /// 清空领域事件集合
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
