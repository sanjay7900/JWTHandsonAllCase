using JWTHandsonAllCase.Models.CommonModel;
using JWTHandsonAllCase.Models.RequestModel;
using JWTHandsonAllCase.Models.ResponseModel;

namespace JWTHandsonAllCase.Repository.TokenServicesRepository
{
    public interface IJWTTokenRepo
    {
        Task<string> GetTokenAsync(UserModel userModel,bool IsRefreshToken);
        Task<string> GetRefreshTokenAsync();
        Task<string> GetRoleNameByIdAsync(int RoleId);
        Task<RefreshTokenResponse> GetTokenByRefreshTokenAsync(RefreshTokenRequest refreshtoken);
    }
}
