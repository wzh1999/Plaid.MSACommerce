using Ocelot.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddOcelot(
    //配置文件路径，采用相对路径
    folder: "./ocelot",
    //表示当前运行环境对象，作用适用于ocelot自动加载环境配置文件
    env: builder.Environment,
    //ocelot表示多个配置文件合并到哪里去，合并配置物理文件(好处可以修改配置文件)、内存文件
    mergeTo: MergeOcelotJson.ToMemory,
    //配置文件是可选
    optional: false,
    //文件发生改变自动加载
    reloadOnChange: true
    );
builder.Services.AddOcelot();
//builder.Services.AddJwtBearer(builder.Configuration);
// Add services to the container.
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

app.Run();

