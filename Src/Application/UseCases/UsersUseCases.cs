using BC = BCrypt.Net.BCrypt;
using PruebaTecnicaMultitenant.Src.Domain.Entities;
using PruebaTecnicaMultitenant.Src.Domain.Services;
using PruebaTecnicaMultitenant.Src.Application.Services;

namespace PruebaTecnicaMultitenant.Src.Application.UseCases
{
    public class UsersUseCases
    {
        private readonly IUsersService _usersService;
        private readonly IAuthTokenGenerator _tokenGenerator;

        public UsersUseCases(IUsersService usersService, IAuthTokenGenerator tokenGenerator)
        {
            _usersService = usersService;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<int> Create(User user)
        {
            user.Password = BC.HashPassword(user.Password);

            return await _usersService.Create(user);
        }

        public string GenerateToken(User user)
        {
            return _tokenGenerator.GenerateToken(user);
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