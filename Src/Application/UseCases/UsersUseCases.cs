using BC = BCrypt.Net.BCrypt;
using PruebaTecnicaMultitenant.Src.Domain.Entities;
using PruebaTecnicaMultitenant.Src.Domain.Services;
using PruebaTecnicaMultitenant.Src.Application.Services;

namespace PruebaTecnicaMultitenant.Src.Application.UseCases
{
    public class UsersUseCases
    {
        private readonly IOrganizationsService _organizationsService;
        private readonly IUsersService _usersService;
        private readonly IAuthTokenGenerator _tokenGenerator;

        public UsersUseCases(
            IUsersService usersService,
            IOrganizationsService organizationsService, 
            IAuthTokenGenerator tokenGenerator)
        {
            _organizationsService = organizationsService;
            _tokenGenerator = tokenGenerator;
            _usersService = usersService;
        }

        //Crea un nuevo usuario validando la existencia de la organización y hasheando su contraseña
        public async Task<int> Create(User user)
        {
            var organization = await _organizationsService.Get(user.Organization.Id);
            if(organization == null)
                throw new Exception("User creation failed. Organization not found");

            user.Organization = organization;

            user.Password = BC.HashPassword(user.Password);

            return await _usersService.Create(user);
        }

        //Genera token de authorización
        public string GenerateToken(User user)
        {
            return _tokenGenerator.GenerateToken(user);
        }

        //Devuelve un usuario dependiendo de su id
        public async Task<User?> Get(int id)
        {
            return await _usersService.Get(id);
        }

        //Valida el inicio de sesión de un usuario
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