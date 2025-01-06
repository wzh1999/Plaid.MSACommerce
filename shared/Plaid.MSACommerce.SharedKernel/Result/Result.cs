using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.MSACommerce.SharedKernel.Result
{
    /// <summary>
    /// 封装返回的类型
    /// 实现IResult接口类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Result<T> : IResult
    {
        /// <summary>
        /// 任意类型构造函数
        /// </summary>
        /// <param name="value"></param>
        protected internal Result(T? value)
        {
            Value = value;
        }
        /// <summary>
        /// 指定返回状态枚举类型构造函数
        /// </summary>
        /// <param name="status"></param>
        protected internal Result(ResultStatus status)
        {
            Status = status;
        }
        /// <summary>
        /// 结果值
        /// </summary>
        public T? Value { get; set; }
        /// <summary>
        /// 是否提交成功
        /// </summary>
        public bool IsSuccess => Status == ResultStatus.Ok;
        /// <summary>
        ///错误，错误消息
        /// </summary>
        public IEnumerable<string>? Errors { get; protected set; }
        /// <summary>
        /// 返回结果状态
        /// </summary>
        public ResultStatus Status { get; protected set; } = ResultStatus.Ok;

        public object? GetValue()
        {
            return Value;
        }

        /// <summary>
        /// 隐式转换处理 
        /// implicit operator关键字
        /// Result<T> 是目标类型，即转换后的类型。
        /// Result 是源类型，即转换前的类型
        /// 转换方法是静态方法，因此必须使用 static 关键字。
        /// </summary>
        /// <param name="result"></param>
        public static implicit operator Result<T>(Result result)
        {
            return new Result<T>(default(T))
            {
                Status = result.Status,
                Errors = result.Errors
            };
        }
    }
    public class Result : Result<Result>
    {
        protected internal Result(Result value) : base(value) { }
        protected internal Result(ResultStatus status) : base(status) { }

        /// <summary>
        /// 表单返回
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Result From(IResult result)
        {
            return new Result(result.Status)
            {
                Errors = result.Errors
            };
        }
        /// <summary>
        /// 提交成功返回
        /// </summary>
        /// <returns></returns>
        public static Result Success()
        {
            return new Result(ResultStatus.Ok);
        }

        /// <summary>
        /// 泛型提交成功返回
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Result<T> Success<T>(T value)
        {
            return new Result<T>(value);
        }

        public static Result Failure()
        {
            return new Result(ResultStatus.Error);
        }
        public static Result Failure(params string[] errors)
        {
            return new Result(ResultStatus.Error)
            {
                Errors = errors.AsEnumerable()
            };
        }

        public static Result NotFound()
        {
            return new Result(ResultStatus.NotFound);

        }
        public static Result NotFound(params string[] error)
        {
            return new Result(ResultStatus.NotFound)
            {
                Errors = error.AsEnumerable()
            };
        }

        public static Result Forbidden()
        {
            return new Result(ResultStatus.Forbidden);
        }

        public static Result Unauthorized()
        {
            return new Result(ResultStatus.Unauthorized);
        }

        public static Result Invalid()
        {
            return new Result(ResultStatus.Invalid);
        }
        /// <summary>
        /// 请求无效返回内容
        /// params 允许你传递多个相同类型的参数，且调用时不需要显式地创建数组。
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static Result Invalid(params string[] errors)
        {
            return new Result(ResultStatus.Invalid)
            {
                Errors = errors.AsEnumerable()
            };
        }
    }
}
