namespace JWTHandsonAllCase.Models.RequestModel
{
    public class UserRequest
    {
        public string UserId {  get; set; } = string.Empty; 
        public string? UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
