using PruebaTecnicaMultitenant.Src.Domain.Entities;

namespace PruebaTecnicaMultitenant.Src.Application.Services
{
    public interface IAuthTokenGenerator
    {
        string GenerateToken(User user);
    }
}