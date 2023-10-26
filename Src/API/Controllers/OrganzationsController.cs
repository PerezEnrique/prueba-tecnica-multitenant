using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaMultitenant.Src.Application.UseCases;
using PruebaTecnicaMultitenant.Src.Domain.Entities;
using PruebaTecnicaMultitenant.Src.Infrastructure;
using PruebaTecnicaMultitenant.Src.Infrastructure.Utils;

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

        //POST http://host:puerto/organizations
        [HttpPost]
        public async Task<ActionResult<OrganizationDto>> Create(CreateOrganizationDto createOrganizationDto)
        {
            try
            {                
                var organization = new Organization()
                {
                    Name = createOrganizationDto.Name,
                    SlugTenant = createOrganizationDto.SlugTenant,
                };

                organization.Id = await _organizationsUseCases.CreateOrganization(organization);

                return CreatedAtAction(nameof(Get), new { id = organization.Id }, organization.AsDto());
            }
            catch (Exception)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        //GET http://host:puerto/organizations?id=1
        [HttpGet]
        public async Task<ActionResult<OrganizationDto>> Get(int id)
        {
            try
            {
                var organization = await _organizationsUseCases.Get(id);
                if(organization == null)
                    return NotFound();

                return organization.AsDto();
            }
            catch (Exception)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}