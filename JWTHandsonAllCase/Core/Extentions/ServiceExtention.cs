using JWTHandsonAllCase.Common;
using JWTHandsonAllCase.Core.Dapper;
using JWTHandsonAllCase.DBContextManager;
using JWTHandsonAllCase.Repository;
using JWTHandsonAllCase.Repository.TokenServicesRepository;
using JWTHandsonAllCase.Services;
using JWTHandsonAllCase.Services.TokenServicesRepository;
using Microsoft.EntityFrameworkCore;

namespace JWTHandsonAllCase.Core.Extentions
{
    public static class ServiceExtention
    {
        public static void AddedServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<JWTDbContext>(s =>
             s.UseSqlServer(configuration.GetConnectionString("JWTHandson"))
           //s.UseMySql(configuration.GetConnectionString("JWTHandson"),ServerVersion.AutoDetect(configuration.GetConnectionString("JWTHandson")))
           ) ;
            services.AddTransient<IJWTTokenRepo, JWTTokenServices>();
            services.AddTransient<IUserRepo,UserServices>();
            services.AddScoped<TokenRevocationServices>();
            services.AddScoped<IDapperQueryService, DapperQueryService>();



           
        }
    }
}
