using Microsoft.Extensions.DependencyInjection;
using Plaid.MSACommerce.HttpApi.Common.Infrastructure;
using Plaid.MSACommerce.HttpApi.Common.Services;
using Plaid.MSACommerce.UseCases.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Plaid.MSACommerce.HttpApi.Common
{
    public static class DependencyInjection
    {
        /// <summary>
        /// 定义扩展方法注册服务入口
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddHttpApiCommon(this IServiceCollection services)
        {
            services.AddScoped<IUser, CurrentUser>();
            //获取应用程序的任何地方访问当前的 HTTP 上下文。
            services.AddHttpContextAccessor();
            services.AddExceptionHandler<UseCaseExceptionHandler>();
            //设置标准 API 返回标准化的错误响应
            services.AddProblemDetails();
            ConfigureCors(services);
            ConfigureSwagger(services);
            return services;
        }

        /// <summary>
        /// 解决跨域问题
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAny", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
        }
        
        /// <summary>
        /// 将swagger 统一成一个，并对这个swagger描述和增加鉴权授权
        /// </summary>
        /// <param name="services"></param>
        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "电商平台 API 文档",
                    Version = "v1",
                    Description = "一个微服务架构的电商平台实战项目"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "JWT Bearer 认证",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new string[] { }
                    }
                });
            });
        }
    }
}
