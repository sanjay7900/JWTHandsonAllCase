using JWTHandsonAllCase.Core.Dapper;
using JWTHandsonAllCase.Core.SqlProvider;
using JWTHandsonAllCase.DBContextManager;
using JWTHandsonAllCase.Models.CommonModel;
using JWTHandsonAllCase.Models.MenuModel;
using JWTHandsonAllCase.Models.RequestModel;
using JWTHandsonAllCase.Repository;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace JWTHandsonAllCase.Services
{
    public class UserServices:IUserRepo
    {
        private readonly IConfiguration _config;
        private readonly JWTDbContext _context;
        private readonly IDapperQueryService _dapper;

        public UserServices(
            IConfiguration configuration,JWTDbContext jWTDbContext,
            IDapperQueryService dapperQueryServices
            )
        {
            _config= configuration; 
            _context= jWTDbContext; 
            _dapper= dapperQueryServices;   
        }
        public async Task<UserModel> CreateAsync(UserModel user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync(); 
            return user;   
        }
        public async Task<UserModel> DeleteAsync(string id)
        {
            var getuser = await _context.Users.FirstOrDefaultAsync(u => u.Email == id);
            if (getuser == null)
            {
                throw new Exception("Invalid username ");
            }
            getuser.IsDeleted = true;   
            _context.Update(getuser);   
            _context.SaveChanges();
            return getuser;
        }
        public async Task<UserModel> GetAsync(string userId, string userPassword)
        {
            var getuser = await _context.Users.FirstOrDefaultAsync(u=>u.Email==userId &&  u.Password==userPassword); 
            if(getuser== null)
            {
                throw new Exception("Invalid username or password");
            }
            return getuser; 
        }
        public async Task<List<Menu>> GetMenuListAsync(int roleid)
        {
            return await _dapper.QueryAsync<Menu>(Procedure.GetAllMenuByRole, new { RoleId = roleid });
        }
        public async Task<List<MenuItem>> GetMenuListByRoleAsync(int RoleId)
        {
            var menuItems = await GetMenuListAsync(RoleId);
            var menuList = new List<MenuItem>();

            foreach (var menu in menuItems.Where(w => w.ParentID == null || w.ParentID == 0))
            {
                var menuItem = new MenuItem
                {
                    Component = menu.Component,
                    MenuItemName = menu.MenuItemName,
                    MenuItemID = menu.MenuItemID,
                    ParentID = 0,
                    Child = new List<MenuItem>()
                };

                AddChild(menu.MenuItemID, menuItem, menuItems);
                menuList.Add(menuItem);
            }

            return menuList; ;
        }
        public async Task<UserModel> GetUserByRefreshTokenAsync(RefreshTokenRequest refreshToken)
        {
            var getuser = await _context.Users.FirstOrDefaultAsync(u =>u.RefreshToken==refreshToken.RefreshToken);
            if (getuser == null)
            {
                throw new Exception("Invalid Refresh token");
            }
            return getuser;
        }
        public async Task<bool> SetRefreshTokenAsync(UserModel user, RefreshTokenRequest refreshTokenRequest)
        {
            var getuser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);
            if (getuser == null)
            {
                throw new Exception("Invalid username or password");
            }
            getuser.RefreshToken = refreshTokenRequest.RefreshToken; 
            _context.Users.Update(user);    
            await _context.SaveChangesAsync(); 
            return true;
        }
        public async Task<UserModel> UpdateAsync(UserModel user)
        {
            var getuser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);
            if (getuser == null)
            {
                throw new Exception("Invalid username or password");
            }
            user.Email = getuser.Email;
            user.Password = getuser.Password;
            user.RoleId = getuser.RoleId;
            user.RefreshToken = getuser.RefreshToken;
            user.ISPhoneConfirmed = getuser.ISPhoneConfirmed;
            user.PhoneNumber = getuser.PhoneNumber;
            user.UserName = getuser.UserName;
            user.Password = getuser.Password;   
            user.Updatedon=DateTime.Now.ToString("dd/mm/yyyy hh:mm:ss") ;    
             _context.Update(user);  

            return getuser;
        }
        private void AddChild(int ParentId, MenuItem parentMenu, List<Menu> menuItems)
        {
            var children = menuItems.Where(w => w.ParentID == ParentId).ToList();
            if (!children.Any())
            {
                return;
            }
            foreach (var child in children)
            {
                var menuItem = new MenuItem
                {
                    Component = child.Component,
                    MenuItemName = child.MenuItemName,
                    MenuItemID = child.MenuItemID,
                    ParentID = child.ParentID,
                    Child = new List<MenuItem>()
                };
                parentMenu.Child.Add(menuItem);
                AddChild(child.MenuItemID, menuItem, menuItems);
            }
        }
    }
}
