using JWTHandsonAllCase.Common;
using JWTHandsonAllCase.Models.ApiCommonResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace JWTHandsonAllCase.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SampleCrudController : ControllerBase
    {
        private static List<Sample> Samples =new List<Sample>();
         
        public SampleCrudController()
        {
            Samples.Add(new Sample()
            {
                Id = 1,
                Author="sanjay",
                Country="india",
                PostalCode="201210",
                Description="nothing",
                Fax="23456789",
                Name="ertyuio",
                Phone="1234567890"
                

            });
            
            
        }


        [HttpGet]
        public IActionResult GetSample()
        {
            if(Samples == null || Samples.Count<=0)
            {
                return new ApiResponseResult<List<Sample>>(

                        new ApiResponse<List<Sample>>(HttpStatusCode.NotFound, ResponseMessage.NotFound, Samples!)

                    );

            }

            var sample = new  ApiResponse<List<Sample>>(HttpStatusCode.OK, ResponseMessage.GetSuccess,Samples);
            return new ApiResponseResult<List<Sample>>(sample);               
        }
    }
    class Sample
    {
        public int Id { get; set; }
        public string Name { get; set; }    = string.Empty;   
        public string Description { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty; 
        public string Phone { get; set; }=string.Empty;
        public string Fax { get; set; } = string.Empty;

    }
}
