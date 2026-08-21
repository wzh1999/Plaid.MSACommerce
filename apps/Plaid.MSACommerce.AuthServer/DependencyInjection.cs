using Plaid.MSACommerce.AuthServer.Apis;
using Plaid.MSACommerce.AuthServer.Clients;
using Plaid.MSACommerce.AuthServer.Services;
using Plaid.MSACommerce.CommonServiceClient;
using Refit;
using StackExchange.Redis;

namespace Plaid.MSACommerce.AuthServer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddHttpApi(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureUserService(services, configuration);
            ConfigureRedis(services, configuration);
            ConfigureIdentity(services, configuration);
            ConfigureCors(services);
            return services;
        }

        private static void ConfigureUserService(IServiceCollection services, IConfiguration configuration)
        {
            #region 绑定注册Refit的Http客户端

            // //读取请求的url地址
            // var userServiceUrl = configuration["ServiceUrls:UserService"];
            // if (userServiceUrl is null) throw new NullReferenceException(nameof(userServiceUrl));
            // //通过HttpClientFactory配置BaseAddress地址,也就是请求url地址
            // services.AddRefitClient<IUserService>()
            //     .ConfigureHttpClient(clinet => { clinet.BaseAddress = new Uri(userServiceUrl); });

            #endregion

            //通用Http请求远程调用服务注入
            // services.AddServiceClient<UserServiceClient>(
            //     option => { option.LoadBalancingStrategy = LoadBalancingStrategy.RoundRobin; },
            //     client => { client.Timeout = TimeSpan.FromSeconds(1); });

            services.AddServiceClient<IUserServiceApi>(option =>
            {
                option.ServiceName = "Plaid.MSACommerce.UserService.HttpApi";
                option.LoadBalancingStrategy = LoadBalancingStrategy.RoundRobin;
            }, client => { client.Timeout = TimeSpan.FromSeconds(1); });
        }

        private static void ConfigureRedis(IServiceCollection services, IConfiguration configuration)
        {
            var redisConn = configuration.GetConnectionString("ConnectionStrings");
            if (redisConn != null)
            {
                services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConn));
            }
        }

        private static void ConfigureIdentity(IServiceCollection services, IConfiguration configuration)
        {
            // services.AddSingleton<IIdentityService, IdentityService>();

            services.AddSingleton<ITokenService, TokenService>();

            //从配置文件中读取JwtSettings,并注入到容器中
            var configurationSection = configuration.GetSection(nameof(JwtSettings));
            var jwtSettings = configuration.Get<JwtSettings>();
            if (jwtSettings is null) throw new NullReferenceException(nameof(jwtSettings));
            services.Configure<JwtSettings>(configurationSection);
        }

        /// <summary>
        /// 通过CORS策略允许所有请求,包括OPTIONS请求,从而解决跨域问题
        /// </summary>
        /// <param name="services"></param>
        private static void ConfigureCors(IServiceCollection services)
        {
            //添加跨域服务策略
            services.AddCors(options =>
            {
                //Allowany(自定义的策略名称)策略,允许所有请求,包括OPTIONS请求
                options.AddPolicy("Allowany", builder =>
                {
                    //允许所有的请求源,方法,头
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
        }
    }
}