using JWTHandsonAllCase.Models.CommonModel;
using JWTHandsonAllCase.Models.MenuModel;
using JWTHandsonAllCase.Models.RequestModel;

namespace JWTHandsonAllCase.Repository
{
    public interface IUserRepo
    {
        Task<UserModel> CreateAsync(UserModel user);    
        Task<UserModel> UpdateAsync(UserModel user);    
        Task<UserModel> DeleteAsync(string id);
        Task<UserModel> GetAsync(string userId,string userPassword);
        Task<UserModel> GetUserByRefreshTokenAsync(RefreshTokenRequest refreshToken);
        Task<bool> SetRefreshTokenAsync(UserModel user, RefreshTokenRequest refreshTokenRequest);
        Task<List<MenuItem>> GetMenuListByRoleAsync(int RoleId);
        Task<List<Menu>> GetMenuListAsync(int roleid);

    }
}
