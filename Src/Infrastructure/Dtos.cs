namespace PruebaTecnicaMultitenant.Src.Infrastructure
{
    public record CreateUserDto(string Email, string Password, int OrganizationId);
    public record UserDto(int Id, string Email, int OrganizationId);
    public record UserCredentialsDto(string Email, string Password);
}