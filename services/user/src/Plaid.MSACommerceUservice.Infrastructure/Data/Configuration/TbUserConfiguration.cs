using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plaid.MSACommerce.Uservice.Core;
using Plaid.MSACommerce.Uservice.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.MSACommerce.Uservice.Infrastructure.Data.Configuration
{
    public class TbUserConfiguration : IEntityTypeConfiguration<TbUser>
    {
        public void Configure(EntityTypeBuilder<TbUser> builder)
        {
            //如果过提示没有ToTal扩展方法需要安装 Microsoft.EntityFrameworkCore,Relational
            //或者安装Mysql或者sql server的第三方ef core操作包也包括该子包
            builder.ToTable("tb_user");//配置实体和表名映射关系

            builder.Property(e => e.Id)
                .HasColumnName("id"); 

            builder.HasIndex(e => e.Username)
                .IsUnique(); //唯一约束

            builder.Property(e => e.Username)
                .IsRequired() //是否为空
                .HasColumnName("username") //数据库字段说明
                .HasMaxLength(DataSchemaConstants.DafeultUsernameMaxLength) //最大长度
                .HasComment("用户名"); //说明

            builder.Property(e => e.Password)
                .IsRequired()
                .HasColumnName("password")
                .HasMaxLength(DataSchemaConstants.DafeultPasswordMaxLength)
                .HasComment("密码,加密存储");

            builder.Property(e => e.Phone)
                .HasColumnName("phone")
                .HasMaxLength(DataSchemaConstants.DafeultPhoneMaxLength)
                .HasComment("注册手机号");

            builder.Property(e => e.Salt)
                .IsRequired()
                .HasColumnName("salt")
                .HasMaxLength(DataSchemaConstants.DafeultSaltMaxLength)
                .HasComment("密码加密的salt的值");
        }
    }
}
