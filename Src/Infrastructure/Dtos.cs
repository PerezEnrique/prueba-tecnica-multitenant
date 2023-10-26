namespace PruebaTecnicaMultitenant.Src.Infrastructure
{
    //Dtos de usuarios
    public record CreateUserDto(string Email, string Password, int OrganizationId);
    public record UserDto(int Id, string Email, OrganiaztionDto Organization);
    public record UserCredentialsDto(string Email, string Password);
    
    //Dtos de organizationes
    public record CreateOrganizationDto(string Name, string SlugTenant);
    public record OrganiaztionDto(int Id, string Name, string SlugTenant);
}