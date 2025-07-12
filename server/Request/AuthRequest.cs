namespace ShopAppApi.Request
{
    public class LoginRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }

    public class LoginGoogleRequest
    {
        public required string IdToken { get; set; }
    }
}