using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PruebaTecnicaMultitenant.Src.Application.UseCases;
using PruebaTecnicaMultitenant.Src.Domain.Entities;
using PruebaTecnicaMultitenant.Src.Infrastructure;
using PruebaTecnicaMultitenant.Src.Infrastructure.Utils;

namespace PruebaTecnicaMultitenant.Src.API.Controllers
{

    [ApiController]
    [Route("/users")]
    public class UsersController : ControllerBase
    {
        private readonly UsersUseCases _usersUseCases;
        public UsersController(UsersUseCases usersUseCases)
        {
            _usersUseCases = usersUseCases;
        }

        //POST http://host:puerto/users
        [HttpPost]
        public async Task<ActionResult<UserDto>> Create(CreateUserDto createUserDto)
        {
            try
            {                
                var user = new User()
                {
                    Email = createUserDto.Email,
                    Password = createUserDto.Password,
                    Organization = new Organization(){
                        Id = createUserDto.OrganizationId
                    }
                };

                user.Id = await _usersUseCases.Create(user);

                var token = _usersUseCases.GenerateToken(user);

                Response.Headers.Add("Authorization", $"Bearer {token}");

                return CreatedAtAction(nameof(Get), new { id = user.Id }, user.AsDto());
            }
            catch (Exception)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        //GET http://host:puerto/users?id=<id>
        [HttpGet]
        public async Task<ActionResult<UserDto>> Get(int id)
        {
            try
            {                
                var user = await _usersUseCases.Get(id);
                if(user == null)
                    return NotFound();

                return user.AsDto();
            }
            catch (Exception)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        //POST http://host:puerto/users/login
        [HttpPost("/users/login")]
        public async Task<ActionResult<UserDto>> LogIn(UserCredentialsDto userCredentials)
        {
            try
            {
                var user = await _usersUseCases.LogIn(userCredentials.Email, userCredentials.Password);

                var token = _usersUseCases.GenerateToken(user);

                Response.Headers.Add("Authorization", $"Bearer {token}");

                return user.AsDto();
            }
            catch (Exception ex)
            {
                if(ex.Message.Contains("Login"))
                    return Problem(statusCode: StatusCodes.Status401Unauthorized);

                return Problem(statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}