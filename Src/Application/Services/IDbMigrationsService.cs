namespace PruebaTecnicaMultitenant.Src.Application.Services
{
    public interface IDbMigrationsService
    {
        Task InitialMigration();
        Task CreateTenantDb(string connectionString);
    }
}