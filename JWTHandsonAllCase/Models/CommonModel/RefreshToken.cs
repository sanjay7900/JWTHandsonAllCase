namespace JWTHandsonAllCase.Models.CommonModel
{
    public class RefreshToken
    {
        public byte[] Token { get; set; }   
        public DateTime ExpireOn { get; set; }  = DateTime.MinValue;    
        public DateTime CreatedOn { get; set;} = DateTime.MinValue;  
    }
}
