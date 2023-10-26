using PruebaTecnicaMultitenant.Src.Domain.Entities;

namespace PruebaTecnicaMultitenant.Src.Infrastructure.Utils
{
    public static class ExtensionMethods
    {
        public static OrganizationDto AsDto(this Organization organization)
        {
            return new OrganizationDto(organization.Id, organization.Name, organization.SlugTenant);
        }

        public static ProductDto AsDto(this Product product)
        {
            return new ProductDto(product.Id, product.Name);
        }

        public static UserDto AsDto(this User user)
        {
            return new UserDto(user.Id, user.Email, user.Organization.AsDto());
        }
    }
}