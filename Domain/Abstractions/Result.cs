using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public T Data { get; set; }

        protected Result(bool isSuccess, IEnumerable<string> errors, T data = default!)
        {
            IsSuccess = isSuccess;
            Errors = errors;
            Data = data;

        }

        public static Result<T> Success(T data)
        {
            return new Result<T>(true, Enumerable.Empty<string>(), data);
        }

        public static Result<T> Success()
        {
            return new Result<T>(true, Enumerable.Empty<string>(), default!);
        }

        public static Result<T> Failure(IEnumerable<string> errors)
        {
            return new Result<T>(false, errors);
        }

        public static Result<T> Failure(string error)
        {
            return new Result<T>(false, new List<string> { error });

        }
    }
}
