using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMLDotNetCore.MiniDigitalWallet.Domain.Model
{
    public class BaseResponseModel
    {
        public string RespCode { get; set; }
        public string RespDesp { get; set; }

        public EnumRespType RespType { get; set; }

        public bool IsSuccess { get; set; }
        public bool IsError { get { return !IsSuccess; } }

        public static BaseResponseModel Success(string RespCode, string RespDesp)
        {
            return new BaseResponseModel()
            {
                IsSuccess = true,
                RespCode = RespCode,
                RespDesp = RespDesp,
                RespType = EnumRespType.Success,
            };
        }

        public static BaseResponseModel ValidationError(string RespCode, string RespDesp)
        {
            return new BaseResponseModel()
            {
                IsSuccess = false,
                RespCode = RespCode,
                RespDesp = RespDesp,
                RespType = EnumRespType.ValidationError,
            };
        }

        public static BaseResponseModel SystemError(string RespCode, string RespDesp)
        {
            return new BaseResponseModel()
            {
                IsSuccess = false,
                RespCode = RespCode,
                RespDesp = RespDesp,
                RespType = EnumRespType.SystemError,
            };
        }
}

        public enum EnumRespType
    {
        None = 0,
        Success,
        ValidationError,
        SystemError,

    }
}
