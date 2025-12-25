using Plaid.MSACommerce.Authentication.JwtBearer;
using Plaid.MSACommerce.HttpApi.Common;
using Plaid.MSACommerce.UserService.HttpApi;
using Plaid.MSACommerce.Uservice.Infrastructure;
using Plaid.MSACommerceUservice.UseCases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//添加数据库配置扩展服务
builder.Services.AddInfrastructure(builder.Configuration);
//添加基础设施层扩展服务
builder.Services.AddUseCase();
//添加Api控制器中扩展服务
builder.Services.AddHttpApi();
builder.Services.AddControllers();
//引入权限验证服务
builder.Services.AddJwtBearer(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
// //添加授权中间件
// app.UseAuthentication();
// app.UseAuthorization();
//添加微服务统一配置中间件入口
app.UseHttpCommon();

app.MapControllers();

app.Run();
