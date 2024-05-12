using JWTHandsonAllCase.Common;
using JWTHandsonAllCase.DBContextManager;
using JWTHandsonAllCase.Models.CommonModel;
using JWTHandsonAllCase.Models.RequestModel;
using JWTHandsonAllCase.Models.ResponseModel;
using JWTHandsonAllCase.Repository;
using JWTHandsonAllCase.Repository.TokenServicesRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JWTHandsonAllCase.Services.TokenServicesRepository
{
    public class JWTTokenServices:IJWTTokenRepo
    {
        private readonly IConfiguration _conf;
        private readonly JWTDbContext _context;
        private readonly IUserRepo _UserContext;

        public JWTTokenServices(IConfiguration configuration,JWTDbContext jWTDbContext,IUserRepo userRepo)
        {
            _conf=configuration;    
            _context=jWTDbContext;  
            _UserContext=userRepo;  
        }

        public async Task<string> GetRefreshTokenAsync()
        {
            var refreshtoken = new RefreshToken()
            {
                Token = RandomNumberGenerator.GetBytes(64),
                ExpireOn = DateTime.UtcNow.AddDays(7),
                CreatedOn = DateTime.UtcNow

            };
            var refreshtokenckecing= JsonConvert.SerializeObject(refreshtoken).Encrypt(_conf["EDKey:key"]);
            try
            {
                await _UserContext.GetUserByRefreshTokenAsync(new RefreshTokenRequest { RefreshToken = refreshtokenckecing });
                await GetRefreshTokenAsync();   
            }
            catch (Exception ex)
            {
                return refreshtokenckecing;
            }

          return refreshtokenckecing;   
           
            
        }

        public async Task<string> GetRoleNameByIdAsync(int RoleId)
        {
             return "User";
        }

        public async Task<string> GetTokenAsync(UserModel userModel, bool IsRefreshToken = false)
        {
            List<Claim> claimslist = new List<Claim>()
            {
                new Claim(ClaimTypes.Role,await GetRoleNameByIdAsync(userModel.RoleId)),
                new Claim(ClaimTypes.Email,userModel.Email),
                new Claim("UniqueId",userModel.UserId),

            };

            ClaimsIdentity identity = new ClaimsIdentity(claimslist);

            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {

                Issuer= _conf["JWT:Issuer"],    
                Audience= _conf["JWT:Audience"],
                Expires=  IsRefreshToken ? DateTime.UtcNow.AddDays(7): DateTime.UtcNow.AddMinutes(5),
                Subject= identity,
                SigningCredentials= new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_conf["JWT:SignInKey"])), SecurityAlgorithms.HmacSha256)

            };
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            SecurityToken jwtSecurityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);    
            
            string token = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
            return token;
        }

        public async Task<RefreshTokenResponse> GetTokenByRefreshTokenAsync(RefreshTokenRequest request)
        {
            if (string.IsNullOrEmpty(request.RefreshToken))
            {
                throw new Exception("Refresh token can't be null or empty");
            }   
            
            string orignaltoken = request.RefreshToken.Decrypt(_conf["EDKey:Key"]);
            var refreshtoken=JsonConvert.DeserializeObject<RefreshToken>(orignaltoken); 
            if(refreshtoken  !=null && refreshtoken.ExpireOn>DateTime.UtcNow)
            {
                var  user = await _UserContext.GetUserByRefreshTokenAsync(request);
                if (user != null)
                {
                    var tokeninfo= new RefreshTokenResponse
                    {
                        RefreshToken = await GetRefreshTokenAsync(),
                        Token = await GetTokenAsync(user),
                    };

                    await _UserContext.SetRefreshTokenAsync(user,new RefreshTokenRequest { RefreshToken=tokeninfo.RefreshToken });  
  
                    return tokeninfo;   
                }
                 

            }
            throw new Exception("No user exist with refresh token");
            

           

        }

       
    }
}
