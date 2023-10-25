namespace PruebaTecnicaMultitenant.Src.Domain.Services
{
    public interface IDbMigrationsService
    {
        Task InitialMigration();
        Task CreateTenantDb(string connectionString);
    }
}