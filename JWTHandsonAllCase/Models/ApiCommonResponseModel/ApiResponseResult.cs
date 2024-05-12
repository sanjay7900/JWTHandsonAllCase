using Microsoft.AspNetCore.Mvc;

namespace JWTHandsonAllCase.Models.ApiCommonResponseModel
{
    public class ApiResponseResult<T> : ObjectResult
    {
        public ApiResponseResult(ApiResponse<T> value) : base(value)
        {
        }
    }
}
