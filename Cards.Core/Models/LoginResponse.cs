namespace Cards.Core.Models
{
    public class LoginResponse : BaseResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
