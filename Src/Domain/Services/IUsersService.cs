using PruebaTecnicaMultitenant.Src.Domain.Entities;

namespace PruebaTecnicaMultitenant.Src.Domain.Services
{
    public interface IUsersService 
    {
        Task<int> Create(User user);
        Task<User?> Get(int id);
        Task<User?> GetByEmail(string email);
    }
}