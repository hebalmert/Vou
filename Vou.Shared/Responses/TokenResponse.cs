namespace Vou.Shared.Responses
{
    public class TokenResponse
    {
        public string Token { get; set; } = null!;

        //public UserResponse? UserDetails { get; set; }

        public DateTime Expiration { get; set; }

        public DateTime ExpirationLocal => Expiration.ToLocalTime();
    }
}
