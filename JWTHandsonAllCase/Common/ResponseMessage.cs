using iText.Html2pdf;
using iText.Kernel.Pdf;
using Microsoft.OpenApi.Models;

namespace JWTHandsonAllCase.Common
{
    public static class ResponseMessage
    {
        public const string Ok = "Success";
        public const string BadRequest = "Invalid request";
        public const string NotFound = "Data not found";
        public const string InternalServerError = "Internal server error";
        public const string WentWorng = "Something went wrong";
        public const string Saved = "Data saved successfully";
        public const string Error = "Error";
        public const string GetSuccess = "Data get successfully";



       public static byte[] bytes(string content) {
            byte[] bytes ;    
            using(var ms=new MemoryStream())
            {


                using(var pw=new PdfWriter(ms))
                {

                    HtmlConverter.ConvertToPdf(content, pw);
                }
                bytes= ms.ToArray();    
            }
            return bytes;   
       }
        


    }
}


