using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWTHandsonAllCase.Models.RequestModel
{
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string UserId { get; set; }  
        public string? UserName { get; set; }
        public string? Password { get; set; } 
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? IsEmailConfirmed { get; set; } 
        public bool? ISPhoneConfirmed { get; set; } 
        public bool IsActive { get; set; }=false;
        public bool IsDeleted { get; set;} =false;
        public bool IsApproved { get; set;} =false;
        public bool IsBlock { get; set; } =false;
        public int RoleId { get; set; } = 0;
        public string Createdon {  get; set; }  =DateTime.Now.ToString("dd/mm/yyyy hh:mm:ss") ;
        public string? Updatedon { get; set; } 
        public string? Deletedon { get; set; }
        public string? RefreshToken {  get; set; }

    }
}
