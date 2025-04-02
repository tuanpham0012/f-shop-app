
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using ShopAppApi.Data;
using ShopAppApi.Helpers.Interfaces;
using ShopAppApi.Request;
using ShopAppApi.ViewModels;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ShopAppApi.Repositories.Auth
{
    public class AuthRepository(IConfiguration _config, ShopAppContext _context, IStringHelper stringHelper) : IAuthRepository
    {
        private readonly IConfiguration configuration = _config;
        private readonly ShopAppContext context = _context;
        private readonly string loginFailMsg = "Sai thông tin đăng nhập!";
        public async Task<LoginInfoVM> Login(LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
            {
                throw new ArgumentException(loginFailMsg);
            }
            var entry = await context.Customers.AsNoTracking().SingleOrDefaultAsync(x => x.Email.Equals(request.Username)) ?? throw new ArgumentException(loginFailMsg);
            Console.WriteLine(entry.Salt);
            bool verifyPassword = stringHelper.VerifyPassword(request.Password, entry.Salt ?? "", entry.Password);

            if (!verifyPassword)
            {
                throw new ArgumentException(loginFailMsg);
            }

            var result = new LoginInfoVM
            {
                Id = entry.Id,
                Name = entry.Name,
                Email = entry.Email,
                Address = entry.Address,
                Phone = entry.Phone,
                Token = GenerateJwtToken(entry)
            };

            return result;
        }
        private string GenerateJwtToken(Customer customer)
        {
            var jwtSettings = configuration.GetSection("Jwt");
            var secretKey = jwtSettings["Key"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var lifetimeMinutes = Convert.ToDouble(jwtSettings["LifetimeMinutes"]);

            if (string.IsNullOrEmpty(secretKey) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
            {
                throw new InvalidOperationException("JWT settings (Key, Issuer, Audience) must be configured.");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, customer.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
                new Claim(ClaimTypes.Name, customer.Name),
            };

            // Tạo Token Descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(lifetimeMinutes),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = credentials
            };

            // Tạo token handler và tạo token string
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token); // Chuyển token thành chuỗi
        }
    }
}