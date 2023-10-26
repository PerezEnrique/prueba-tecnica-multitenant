using BC = BCrypt.Net.BCrypt;
using PruebaTecnicaMultitenant.Src.Domain.Entities;
using PruebaTecnicaMultitenant.Src.Domain.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PruebaTecnicaMultitenant.Src.Infrastructure.Utils.Constants;

namespace PruebaTecnicaMultitenant.Src.Application.UseCases
{
    public class UsersUseCases
    {
        private readonly IConfiguration _configuration;
        private readonly IUsersService _usersService;

        public UsersUseCases(IUsersService usersService, IConfiguration configuration)
        {
            _configuration = configuration;
            _usersService = usersService;
        }

        public async Task<int> Create(User user)
        {
            user.Password = BC.HashPassword(user.Password);

            return await _usersService.Create(user);
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:JwtKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new []{
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(ClaimNames.OrganizationId, user.OrganizationId.ToString())
                }),
                Expires = DateTime.UtcNow.Add(TimeSpan.FromHours(1)),
                SigningCredentials = new SigningCredentials(
                    key: new SymmetricSecurityKey(key),
                    algorithm: SecurityAlgorithms.HmacSha256Signature
                    )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<User?> Get(int id)
        {
            return await _usersService.Get(id);
        }

        public async Task<User> LogIn(string email, string password)
        {
            var user = await _usersService.GetByEmail(email);
            if (user == null)
                throw new Exception("Login failed");

            var validPassword = BC.Verify(password, user.Password);
            if (!validPassword)
                throw new Exception("Login failed");

            return user;            
        }
    }
}