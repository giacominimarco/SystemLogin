using ProjetoLogin.API.Models;
using ProjetoLogin.API.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace ProjetoLogin.API.Services
{
    public class UserService
    {
        private readonly IUserRepository _repo;
        private readonly IConfiguration _config;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository repo, IConfiguration config)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _passwordHasher = new PasswordHasher<User>();
        }

        public User? Authenticate(string username, string password)
        {
            User? user = _repo.GetByUsername(username);
            if (user != null && _passwordHasher.VerifyHashedPassword(user, user.Password, password) == PasswordVerificationResult.Success)
            {
                return user;
            }

            return null;
        }

        public List<User> GetUsersPaged(int page, int pageSize)
        {
            return _repo.GetAll(page, pageSize);
        }

        public int GetTotalCount()
        {
            return _repo.Count();
        }

        public string GenerateJwtToken(User user)
        {
            Claim[] claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("id", user.Id.ToString())
            };

            IConfigurationSection jwtSettings = _config.GetSection("JwtSettings");

            string secretKey = jwtSettings.GetValue<string>("SecretKey") ?? throw new ArgumentNullException("SecretKey", "A chave JWT não está definida no appsettings.json.");
            string issuer = jwtSettings.GetValue<string>("Issuer") ?? throw new ArgumentNullException("Issuer", "Issuer não está definido no appsettings.json.");
            string audience = jwtSettings.GetValue<string>("Audience") ?? throw new ArgumentNullException("Audience", "Audience não está definido no appsettings.json.");
            int expiresInMin = jwtSettings.GetValue<int>("ExpiresInMinutes");

            byte[] secretBytes = Encoding.UTF8.GetBytes(secretKey);
            SymmetricSecurityKey key = new SymmetricSecurityKey(secretBytes);
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresInMin),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}