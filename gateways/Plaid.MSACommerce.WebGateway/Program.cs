using Ocelot.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
//用于加载多配置管理器的
builder.Configuration.AddOcelot(
    //指定ocelot指定的路径，所在的文件夹根目录，指定的是相对路径
    folder: "./ocelot",
    //表示传入当前的环境对象，自动选择根据环境加载对应的配置文件
    env: builder.Environment,
    //表示ocelot的配置json文件合并方式，合并到哪里去？
    //配置多个ocelot.json文件时在启动的时候将合并成一个文件，两种方式
    //1.合并成物理文件--会自动生成物理的完整配置文件
    //2.合并到内存 .ToMemory
    mergeTo: MergeOcelotJson.ToMemory,
    //表示配置文件是可选的
    optional: false,
    //文件发生改变后可以自动重新加载
    reloadOnChange: true
    );
builder.Services.AddOcelot();
//builder.Services.AddJwtBearer(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //配置集成ocelot后的swaggerui展示的接口内容
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Gateway V1");
        options.SwaggerEndpoint("/auth/swagger.json", "AuthServer V1");
        options.SwaggerEndpoint("/user/swagger.json", "UserService V1");
    });
}

app.Run();

