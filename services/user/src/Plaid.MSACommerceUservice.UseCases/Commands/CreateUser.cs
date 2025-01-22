

using Microsoft.EntityFrameworkCore;
using Plaid.MSACommerce.Uservice.Core.Entites;
using Plaid.MSACommerce.Uservice.Infrastructure.Tools;

namespace Plaid.MSACommerce.Uservice.UseCases.Commands
{
    /// <summary>
    /// 定义一个接受命令请求对象 继承MediatR中请求接口\
    /// record 表示用于定义一个不可变类型对象
    /// </summary>
    /// <param name="Username">用户名</param>
    /// <param name="Password">密码</param>
    /// <param name="Phone">手机号</param>
    public record CreateUserCommand(string Username, string Password, string? Phone) : ICommand<Result>;

    /// <summary>
    /// 定义验证器 用于验证命令请求的参数
    /// </summary>
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(command => command.Username)
                .NotEmpty()
                .MaximumLength(DataSchemaConstants.DafeultUsernameMaxLength);

            RuleFor(command => command.Password)
                .NotEmpty()
                .MinimumLength(DataSchemaConstants.DafeultPasswordMinLength)
                .MaximumLength(DataSchemaConstants.DafeultPasswordMaxLength);
            RuleFor(command => command.Phone)
                .Length(DataSchemaConstants.DafeultPhoneMaxLength);
        }
    }
    /// <summary>
    /// 创建用户命令处理器
    /// 继承MeditaR封装的接口
    /// </summary>
    /// <param name="dbContext">数据上下文</param>
    /// <param name="mapper">Automapper映射</param>
    public class CreateUserCommandHanlder(UserDbContext dbContext, IMapper mapper) : ICommandHandler<CreateUserCommand, Result>
    {
        /// <summary>
        /// 命令处理逻辑
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <param name="cancellationToken">MeditaR提供异步取消机制</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userExists = await dbContext.TbUser.AnyAsync(user => user.Username == request.Username, cancellationToken: cancellationToken);
            if (userExists)
            {
                return Result.Failure("用户名已存在");
            }

            var user = mapper.Map<TbUser>(request);

            user.Salt = MD5Helper.MD5EncodingOnly(user.Username);
            user.Password = MD5Helper.MD5EncodingWithSalt(user.Password, user.Salt);

            dbContext.TbUser.Add(user);
            var count = await dbContext.SaveChangesAsync(cancellationToken);

            return count != 1 ? Result.Failure("用户注册失败") : Result.Success();
        }
    }
}
