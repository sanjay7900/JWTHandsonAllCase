using JWTHandsonAllCase.Common;
using JWTHandsonAllCase.DBContextManager;
using JWTHandsonAllCase.Models.ApiCommonResponseModel;
using JWTHandsonAllCase.Models.MenuModel;
using JWTHandsonAllCase.Models.RequestModel;
using JWTHandsonAllCase.Repository;
using JWTHandsonAllCase.Repository.TokenServicesRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.Net;
using System.Security.Claims;

namespace JWTHandsonAllCase.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IJWTTokenRepo _JwtContext;
        private readonly IUserRepo _userContext;
        private readonly JWTDbContext _dbContext;

        public UserController(IJWTTokenRepo jWTTokenRepo, IUserRepo userRepo,JWTDbContext dbContext)
        {
            _JwtContext = jWTTokenRepo;
            _userContext = userRepo;

            _dbContext = dbContext;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser(UserCreateRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            var user = _dbContext.Users.FirstOrDefault(u=>u.Email.Equals(request.EmailId)); 
            if (user != null)
            {
                throw new Exception("User alredy exist with email id");

            }
            user = new UserModel()
            {
                Email = request.EmailId,
                Password = request.Password,
                IsApproved = true,
                IsActive = true,
                IsDeleted = false,
                RoleId = 10,
                Createdon=DateTime.Now.ToString("dd/mm/yyyy hh:mm:ss"), 

            };
            
            
            var createuser= await   _userContext.CreateAsync(user);
            if (createuser == null)
            {
                throw new Exception("Something went wrong");
            }
            return Ok("user  created successfully");

        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserRequest userRequest)
        {
            if (userRequest == null)
            {
                return BadRequest("Invalid request");
            }
            if (string.IsNullOrEmpty(userRequest.UserId) || string.IsNullOrEmpty(userRequest.Password))
            {
                return BadRequest("UserId or Password incorrect");
            }

             var  user= await   _userContext.GetAsync(userRequest.UserId, userRequest.Password);

            string token = await _JwtContext.GetTokenAsync(user,false);
            string refreshtoken = await _JwtContext.GetRefreshTokenAsync();
            await _userContext.SetRefreshTokenAsync(user,new RefreshTokenRequest { RefreshToken=refreshtoken});

            return  Ok(new {
               Token= token, 
               RefreshToken= refreshtoken });

        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GetTokenByRefreshTokenAsync(RefreshTokenRequest  request)
        {

            var data = await _JwtContext.GetTokenByRefreshTokenAsync(request);
            return Ok(data);     
        }
        [HttpPost]
        [Authorize(Roles ="User")]
        public async Task<IActionResult> IsRoleAuth()
        {
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> GetMenuByRoleIdAsync(int role)
        {
         var  data= await _userContext.GetMenuListByRoleAsync(role);
            if (data == null || data.Count <= 0)
            {
                return new ApiResponseResult<List<MenuItem>>(

                        new ApiResponse<List<MenuItem>>(HttpStatusCode.NotFound, ResponseMessage.NotFound, data!)

                    );

            }

            var sample = new ApiResponse<List<MenuItem>>(HttpStatusCode.OK, ResponseMessage.GetSuccess,  data);
            return new ApiResponseResult<List<MenuItem>>(sample);
        }
    }
}
