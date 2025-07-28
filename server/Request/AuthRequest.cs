using System.ComponentModel.DataAnnotations;

namespace ShopAppApi.Request
{
    public class LoginRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }

    public class LoginGoogleRequest
    {
        [Required]
        public string? Token { get; set; }
        [Required]
        public TokenType Type { get; set; }

    }

    public enum TokenType
        {
            Code = 1,
            Token = 2,
            Id = 3
        }
}