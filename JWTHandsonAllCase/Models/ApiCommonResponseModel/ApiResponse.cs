using Newtonsoft.Json;
using System.Net;

namespace JWTHandsonAllCase.Models.ApiCommonResponseModel
{
    public class ApiResponse<T>
    {
        public ApiResponse()
        {
            
        }
        public ApiResponse(HttpStatusCode statusCode,string Message,T Data) 
        {
            this.StatusCode = (int)statusCode;
            this.Message = Message;
            this.Data = Data;
        }
        public T Data { get; set; }
        public string Message {  get; set; }    
        public int StatusCode { get; set; }
        public override string ToString()
        {
           return JsonConvert.SerializeObject(this);  
        }
    }
}
