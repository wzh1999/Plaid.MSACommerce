using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Plaid.MSACommerce.SharedKernel.Domain;
using Plaid.MSACommerce.UseCases.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.MSACommerce.Infrastructure.EntityFrameworkCore.Interceptors
{
    /// <summary>
    /// 审计实体拦截器
    /// 通过继承SaveChangesInterceptor拦截DbContext的保存操作
    /// </summary>
    /// <param name="currentUser">注入IUser信息语法糖写法</param>
    public class AuditEntityInterceptor(IUser currentUser) : SaveChangesInterceptor
    {
        /// <summary>
        /// 拦截实体操作保存提交事件
        /// </summary>
        /// <param name="eventData">拦截到数据</param>
        /// <param name="result">返回受影响行数</param>
        /// <returns></returns>
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);
            //返回提交数据
            return base.SavingChanges(eventData, result);
        }
        /// <summary>
        /// 拦截实体操作异步保存提交事件
        /// </summary>
        /// <param name="eventData">拦截到数据</param>
        /// <param name="result">返回受影响行数</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        /// <summary>
        /// 更新冗余字段
        /// </summary>
        /// <param name="context">数据库上下文</param>
        public void UpdateEntities(DbContext? context)
        {
            //判断数据上下文是否为空
            if (context == null)
            {
                return;
            }
            //新增和修改的时候更新审计时间和最后修改审计时间
            foreach (var entry in context.ChangeTracker.Entries<BaseAuditEntity>())
            {
                if (entry.State is not (EntityState.Added or EntityState.Modified))
                {
                    continue;
                }
                var now = DateTimeOffset.Now;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = now;
                    entry.Entity.LastModifiedAt = now;
                }
                else
                {
                    entry.Entity.LastModifiedAt = now;
                }

            }

            //新增和修改的时候更新审计人和最后修改人
            foreach (var entry in context.ChangeTracker.Entries<AuditwithUserEntity>())
            {
                if (entry.State is not (EntityState.Added or EntityState.Modified))
                {
                    continue;
                }
                if (currentUser.Id is null)
                {
                    continue;
                }
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreateBy = currentUser.Id;
                }
                else
                {
                    entry.Entity.LastModifiedBy = currentUser.Id;
                }
            }
        }
    }
}
