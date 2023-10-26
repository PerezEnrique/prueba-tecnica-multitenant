using PruebaTecnicaMultitenant.Src.Domain.Entities;

namespace PruebaTecnicaMultitenant.Src.Infrastructure.Utils
{
    public static class ExtensionMethods
    {
        public static UserDto AsDto(this User user)
        {
            return new UserDto(user.Id, user.Email, user.Organization.AsDto());
        }

        public static OrganiaztionDto AsDto(this Organization organization)
        {
            return new OrganiaztionDto(organization.Id, organization.Name, organization.SlugTenant);
        }
    }
}