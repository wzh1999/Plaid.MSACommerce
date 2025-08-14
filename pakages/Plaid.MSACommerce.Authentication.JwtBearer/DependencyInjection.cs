using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Plaid.MSACommerce.Authentication.JwtBearer;

public static class DependencyInjection
{
    /// <summary>
    /// 进行jwt鉴权
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <exception cref="NullReferenceException"></exception>
    public static void AddJwtBearer(this IServiceCollection services, IConfiguration configuration)
    {
        //从配置文件中读取JwtSettings，并注入到容器中
        var configurationSection=configuration.GetSection("JwtBearer");
        var jwtSettings = configurationSection.Get<JwtSettings>();
        if (jwtSettings is null) throw new NullReferenceException(nameof(jwtSettings));
        //注入依赖注入中
        services.Configure<JwtSettings>(configurationSection);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
            options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Secret)
                        )
                };
            });
    }
}