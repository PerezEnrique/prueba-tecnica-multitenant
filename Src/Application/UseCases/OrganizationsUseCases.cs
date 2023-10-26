using PruebaTecnicaMultitenant.Src.Application.Services;
using PruebaTecnicaMultitenant.Src.Domain.Entities;
using PruebaTecnicaMultitenant.Src.Domain.Services;

namespace PruebaTecnicaMultitenant.Src.Application.UseCases
{
    public class OrganizationsUseCases
    {
        private readonly IDbMigrationsService _dbMigrationsService;
        private readonly IOrganizationsService _organizationsService;
        public OrganizationsUseCases(IOrganizationsService organizationsService, IDbMigrationsService dbMigrationsService)
        {
            _dbMigrationsService = dbMigrationsService;
            _organizationsService = organizationsService;
        }

        public async Task<int> CreateOrganization(Organization organization)
        {
            var createdOrganizationId = await _organizationsService.Create(organization);

            await _dbMigrationsService.CreateTenantDb(organization.SlugTenant);

            return createdOrganizationId;
                
        }
        public async Task<Organization?> Get(int id)
        {
            return await _organizationsService.Get(id);
        }
    }
}