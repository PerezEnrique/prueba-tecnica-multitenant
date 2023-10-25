using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaMultitenant.Src.Application.UseCases;
using PruebaTecnicaMultitenant.Src.Domain.Entities;

namespace PruebaTecnicaMultitenant.Src.API.Controllers
{
    [ApiController]
    [Route("/organizations")]
    public class OrganizationsController : ControllerBase
    {
        private readonly OrganizationsUseCases _organizationsUseCases;
        public OrganizationsController(OrganizationsUseCases organizationsUseCases)
        {
            _organizationsUseCases = organizationsUseCases;
        }

        [HttpPost]
        public async Task<ActionResult<Organization>> Add(Organization organization)
        {
            var id = await _organizationsUseCases.CreateOrganization(organization);

            //Temporarily to test project pipeline
                return new Organization(){
                Id = id,
                Name = organization.Name,
                SlugTenant = organization.SlugTenant,
            };
        }
    }
}