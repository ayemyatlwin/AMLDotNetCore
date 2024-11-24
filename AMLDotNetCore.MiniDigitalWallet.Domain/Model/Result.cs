using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMLDotNetCore.MiniDigitalWallet.Domain.Model
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public bool IsError { get; set; }

        public bool IsValidationError { get { return type == EnumRespType.ValidationError; } }

        public bool IsSystemError { get { return type == EnumRespType.SystemError; } }


        public EnumRespType type { get; set; }

        public T Data { get; set; }

        public string Message { get; set; }

        public static Result<T> Success(T data, string message ="Success") 
        {
            return new Result<T>()
            {
                IsSuccess = true,
                type = EnumRespType.Success,
                Data = data,
                Message = message
            };
            
        }

        public static Result<T> ValidationError(string message,T? data = default)
        {
            return new Result<T>()
            {
                IsSuccess = false,
                type = EnumRespType.ValidationError,
                Data = data,
                Message = message
            };

        }

        public static Result<T> SystemError(string message, T? data = default)
        {
            return new Result<T>()
            {
                IsSuccess = false,
                type = EnumRespType.SystemError,
                Data = data,
                Message = message
            };

        }
    }
}
