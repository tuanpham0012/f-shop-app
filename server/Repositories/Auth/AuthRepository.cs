
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Google.Apis.Auth;
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
                new Claim("ID", customer.Id.ToString()),
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

        public async Task<LoginInfoVM> GetInfo(long customerId)
        {
            var entry = await context.Customers.AsNoTracking().SingleOrDefaultAsync(x => x.Id == customerId) ?? throw new ArgumentException("Không tìm thấy thông tin người dùng!");
            var result = new LoginInfoVM
            {
                Id = entry.Id,
                Name = entry.Name,
                Email = entry.Email,
                Address = entry.Address,
                Phone = entry.Phone,
            };
            return result;
        }

        public async Task<LoginInfoVM> LoginWithGoogle(string idToken)
        {
            if (string.IsNullOrWhiteSpace(idToken))
            {
                throw new ArgumentException("ID token không được để trống.");
            }

            var payload = await VerifyGoogleTokenAsync(idToken);
            if (payload == null)
            {
                throw new UnauthorizedAccessException("Invalid Google token.");
            }
            // Kiểm tra xem email đã tồn tại trong hệ thống chưa
            var customer = await context.Customers.AsNoTracking().SingleOrDefaultAsync(c => c.Email == payload.Email);
            if (customer == null)
            {
                // Nếu chưa tồn tại, tạo mới người dùng
                customer = new Customer
                {
                    Email = payload.Email,
                    Name = payload.Name ?? $"{payload.FamilyName} {payload.GivenName}",
                    Address = string.Empty, // Hoặc giá trị mặc định khác
                    Phone = string.Empty,   // Hoặc giá trị mặc định khác
                    Salt = string.Empty,    // Không cần salt cho đăng nhập Google
                    Password = string.Empty,  // Không cần password cho đăng nhập Google
                    Status = 1,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                context.Customers.Add(customer);
                await context.SaveChangesAsync();
            }

            return new LoginInfoVM
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Address = customer.Address,
                Phone = customer.Phone,
                Token = GenerateJwtToken(customer) // Tạo JWT token cho người dùng
            };
        }


        private async Task<GoogleJsonWebSignature.Payload> VerifyGoogleTokenAsync(string idToken)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { configuration["SocialAuthentication:Google:ClientId"] ?? throw new UnauthorizedAccessException("Invalid ClientId") }   // bắt buộc khớp aud
            };

            try
            {
                return await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
            }
            catch (InvalidJwtException ex)
            {
                throw new UnauthorizedAccessException("Invalid Google token", ex);
            }
        }
    }
}