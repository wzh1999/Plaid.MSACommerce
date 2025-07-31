using Ocelot.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddOcelot(
    //�����ļ�·�����������·��
    folder: "./ocelot",
    //��ʾ��ǰ���л�����������������ocelot�Զ����ػ��������ļ�
    env: builder.Environment,
    //ocelot��ʾ��������ļ��ϲ�������ȥ���ϲ����������ļ�(�ô������޸������ļ�)���ڴ��ļ�
    mergeTo: MergeOcelotJson.ToMemory,
    //�����ļ��ǿ�ѡ
    optional: false,
    //�ļ������ı��Զ�����
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

