using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Text;

namespace JWTHandsonAllCase.Core.Extentions
{
    public static class JWTServicesExtention
    {
        public static IServiceCollection AddJwtServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(j =>
            {
                j.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                j.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                j.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience=true,
                    ValidateLifetime = true,
                    ValidAudience = configuration["JWT:Audience"].ToString(),
                    ValidIssuer = configuration["JWT:Issure"].ToString(),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SignInKey"])),
                };
               
                



            });
            return services;

        }

    }
}
