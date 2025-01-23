using Plaid.MSACommerce.UserService.HttpApi;
using Plaid.MSACommerce.Uservice.Infrastructure;
using Plaid.MSACommerceUservice.UseCases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//������ݿ�������չ����
builder.Services.AddInfrastructure(builder.Configuration);
//��ӻ�����ʩ����չ����
builder.Services.AddUseCase();
//���Api����������չ����
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
