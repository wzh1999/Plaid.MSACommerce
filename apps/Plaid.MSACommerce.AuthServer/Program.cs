using Consul.AspNetCore;
using Plaid.MSACommerce.AuthServer;
using Plaid.MSACommerce.Consul.ServiceRegistration;

var builder = WebApplication.CreateBuilder(args);

//读取配置文件中ServiceCheck 节点下所有的配置信息，并将配置信息映射到ServiceCheckConfiguration中
var serviceCheck=builder.Configuration.GetSection("ServiceCheck").Get<ServiceCheckConfiguration>();
//如果配置文件中没有对应的配置信息 则实例化一个默认的ServiceCheckConfiguration健康检查配置信息
serviceCheck ??= new ServiceCheckConfiguration();

//创建注入Consul服务 必须先注册
builder.Services.AddConsul();
//明确链接地址，怎么服务注册到consul便于服务与consul进行通信
builder.Services.AddConsulService(serviceConfiguration =>
{
    serviceConfiguration.ServiceAddress =
        new Uri(builder.Configuration["urls"] ?? builder.Configuration["applicationUrl"]);
}, serviceCheck);

//将consul健康检查服务注入到DI容器中 便于后期通过地址http://地址/health进行健康检查 与下面使用app.UseHealthChecks((serviceCheck.Path));
//serviceCheck.Path这个默认的就是/health
builder.Services.AddHealthChecks();

// Add services to the container.
builder.Services.AddHttpApi(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//提供端点出去用于建立心跳检查
app.UseHealthChecks((serviceCheck.Path));

app.UseAuthorization();

app.MapControllers();

app.Run();
