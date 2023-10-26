namespace PruebaTecnicaMultitenant.Src.Infrastructure
{
    //Dtos de usuarios
    public record CreateUserDto(string Email, string Password, int OrganizationId);
    public record UserDto(int Id, string Email, OrganizationDto Organization);
    public record UserCredentialsDto(string Email, string Password);
    
    //Dtos de organizationes
    public record CreateOrganizationDto(string Name, string SlugTenant);
    public record OrganizationDto(int Id, string Name, string SlugTenant);

    //Dtos de productos
    public record CreateProductDto(string Name);
    public record ProductDto(int Id, string Name);
}