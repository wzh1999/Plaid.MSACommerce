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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
