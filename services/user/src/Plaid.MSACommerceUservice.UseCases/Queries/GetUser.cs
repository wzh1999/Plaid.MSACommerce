using Microsoft.EntityFrameworkCore;
using Plaid.MSACommerce.Uservice.Infrastructure.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.MSACommerce.Uservice.UseCases.Queries
{
    /// <summary>
    /// 定义返回用户数据Dto
    /// </summary>
    /// <param name="Id">用户id</param>
    /// <param name="Username">用户名称</param>
    /// <param name="Phone">手机号</param>
    public record UserDto(long Id, string Username, string? Phone);

    /// <summary>
    /// 定义命令请求参数,响应内容
    /// </summary>
    /// <param name="UserName">用户名</param>
    /// <param name="Password">密码</param>
    public record GetUserQuery(string UserName, string Password) : IQuery<Result<UserDto>>;

    /// <summary>
    /// 验证器
    /// </summary>
    public class GetQuestionQueryValidator : AbstractValidator<GetUserQuery>
    {
        public GetQuestionQueryValidator()
        {
            RuleFor(query => query.UserName)
                .NotEmpty();

            RuleFor(query => query.Password)
                 .NotEmpty();
        }
    }
    /// <summary>
    /// 查询命令处理器
    /// </summary>
    /// <param name="dbContext">数据库上下文</param>
    /// <param name="mapper">映射帮助类</param>
    public class GetUserQueryHandler(UserDbContext dbContext, IMapper mapper) : IQueryHandler<GetUserQuery, Result<UserDto>>
    {
        public async Task<Result<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            //AsNoTracking用于禁止跟踪实体代理,减少内存开销,只适用于查询不对数据库修改的操作
            var user = await dbContext.TbUser.AsNoTracking().Where(tbUser => tbUser.Username == request.UserName).FirstOrDefaultAsync(cancellationToken: cancellationToken);
            if (user == null)
            {
                return Result.NotFound();
            }
            if (MD5Helper.MD5EncodingWithSalt(request.Password, user.Salt) != user.Password)
            {
                return Result.Failure("密码不正确");
            }

            var userDto = mapper.Map<UserDto>(user);
            return Result.Success(userDto);
        }
    }
}
