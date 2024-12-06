using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.MSACommerce.SharedKernel.Domain
{
    /// <summary>
    /// 定义实体基础接口
    /// </summary>
    public interface IEntity;

    /// <summary>
    /// 定义基础实体继承接口
    /// </summary>
    /// <typeparam name="TId">实体必须存在Id</typeparam>
    public interface IEntity<TId>:IEntity
    {
        TId Id { get; set; }
    }
}
