# EF Core 执行迁移命令踩坑教程

## 1.NuGet包管理

```c#
//需要安装
Microsoft.EntityFrameworkCore.Tools
//Tools已经包含Design NuGet包
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore
//安装对应的数据库辅助NuGet包
Pomelo.EntityFrameworkCore.MySql
//注意安装引用三个Nuget包的版本要一直，同时对应的数据库辅助包版本要支持EF Core对应的版本
```

## 2执行命令

```c#
//先安装dotnet-ef 命令包
dotnet tool install --global dotnet-ef

//验证是否安装成功
dotnet ef --version

//执行迁移命令
//针对多个分布式、微服务结构
//--project ".\services\user\src\Plaid.MSACommerceUservice.Infrastructure" 包含DbContext的项目（数据库DbContext层、实体，需要安装Nuget包Microsoft.EntityFrameworkCore、Pomelo.EntityFrameworkCore.MySql）
//--startup-project ".\services\user\src\Plaid.MSACommerce.UserService.HttpApi\Plaid.MSACommerce.UserService.HttpApi.csproj" 指定程序启动入口(包含数据库字段配置链接，EF Core服务注入；Nuget包需要安装Microsoft.EntityFrameworkCore.Tools)
dotnet ef migrations add Plaid --project ".\services\user\src\Plaid.MSACommerceUservice.Infrastructure" --startup-project ".\services\user\src\Plaid.MSACommerce.UserService.HttpApi\Plaid.MSACommerce.UserService.HttpApi.csproj"
    
//更新实体到数据库创建表
//分层的情况下也需要指定具体的路径
dotnet ef database update --project ".\services\user\src\Plaid.MSACommerceUservice.Infrastructure" --startup-project ".\services\user\src\Plaid.MSACommerce.UserService.HttpApi\Plaid.MSACommerce.UserService.HttpApi.csproj"
```

