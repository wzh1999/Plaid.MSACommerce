using Microsoft.EntityFrameworkCore;
using Plaid.MSACommerce.Uservice.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.MSACommerce.Uservice.Infrastructure.Data
{
    /// <summary>
    /// 定义数据交互上下文
    /// (DbContextOptions<UserDbContext> options) : DbContext(options)显示构造自己数据库上下文
    /// </summary>
    /// <param name="options">用于构造注入配置数据库链接字符</param>
    public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
    {
        //配置实体
        public DbSet<TbUser> TbUser => Set<TbUser>();

        /// <summary>
        /// 特性映射,数据注释
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //通过实现提取到专门的类中继承IEntityTypeConfiguration 从而减少这个方法的内容
            //自动加载并应用当前程序集中的所有 IEntityTypeConfiguration<T> 配置
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
