using PruebaTecnicaMultitenant.Src.Domain.Entities;

namespace PruebaTecnicaMultitenant.Src.Domain.Services
{
    public interface IOrganizationsService
    {
        Task<int> Create(Organization organization);
        Task<Organization?> Get(int id);
    }
}