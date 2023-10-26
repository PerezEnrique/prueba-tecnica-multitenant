using PruebaTecnicaMultitenant.Src.Domain.Entities;

namespace PruebaTecnicaMultitenant.Src.Infrastructure.Utils
{
    public static class ExtensionMethods
    {
        public static UserDto AsDto(this User user)
        {
            return new UserDto(user.Id, user.Email, user.OrganizationId);
        }
    }
}