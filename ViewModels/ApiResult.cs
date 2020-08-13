using Common.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace ViewModels
{
    public class ApiResult
    {
        public bool IsSuccess { get; set; }
        public ApiResultStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public ApiResult(bool isSuccess, ApiResultStatusCode statusCode, string message = null)
        {
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            Message = message ?? statusCode.ToString();
        }
        public static implicit operator ApiResult(OkResult result)
        {
            return new ApiResult(true, ApiResultStatusCode.Success, "اوکی");
        }
        public static implicit operator ApiResult(BadRequestResult result)
        {
            return new ApiResult(false, ApiResultStatusCode.BadRequest, "BadRequestResult");
        }
        public static implicit operator ApiResult(bool result)
        {
            return new ApiResult(result, ApiResultStatusCode.Success, result.ToString());
        }
        public static implicit operator ApiResult(string result)
        {
            return new ApiResult(true, ApiResultStatusCode.Success, result.ToString());
        }
    }

    public class ApiResult<TData> : ApiResult where TData : class
    {
        public TData Data { get; set; }
        public ApiResult(bool isSuccess, ApiResultStatusCode statusCode, TData data, string message = null) : base(isSuccess, statusCode, message)
        {
            Data = data;
        }
        public static implicit operator ApiResult<TData>(TData result)
        {
            return new ApiResult<TData>(true, ApiResultStatusCode.Success, result, "users");
        }
        public static implicit operator ApiResult<TData>(int result)
        {
            return new ApiResult<TData>(true, ApiResultStatusCode.Success, null, result.ToString());
        }
        public static implicit operator ApiResult<TData>(BadRequestResult result)
        {
            return new ApiResult<TData>(false, ApiResultStatusCode.BadRequest, null, result.ToString());
        }
        public static implicit operator ApiResult<TData>(OkResult result)
        {
            return new ApiResult<TData>(true, ApiResultStatusCode.Success, null, "Create");
        }
        public static implicit operator ApiResult<TData>(bool result)
        {
            return new ApiResult<TData>(true, ApiResultStatusCode.Success, null, "true");
        }
    }

    public enum ApiResultStatusCode
    {
        [Display(Name = "عملیات با موفقیت انجام شد")]
        Success = 0,

        [Display(Name = "خطایی در سرور رخ داده است")]
        ServerError = 1,

        [Display(Name = "پارامتر های ارسالی معتبر نیستند")]
        BadRequest = 2,

        [Display(Name = "یافت نشد")]
        NotFound = 3,

        [Display(Name = "لیست خالی است")]
        ListEmpty = 4,

        [Display(Name = "خطایی در پردازش رخ داد")]
        LogicError = 5,

        [Display(Name = "خطای احراز هویت")]
        UnAuthorized = 6,

        [Display(Name = "Created Data Success")]
        CreatedData = 7

    }
}
