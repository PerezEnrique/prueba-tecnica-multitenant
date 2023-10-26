using Dapper;
using Microsoft.Data.Sqlite;
using PruebaTecnicaMultitenant.Src.Domain.Services;

namespace PruebaTecnicaMultitenant.Src.Infrastructure.Services
{
    public class DbMigrationsService : IDbMigrationsService
    {
        private readonly IConfiguration _configuration;

        public DbMigrationsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task InitialMigration()
        {
            var connectionString = _configuration.GetConnectionString("MainConnection");
            using var connection = new SqliteConnection(connectionString);

            var sql = @"CREATE TABLE IF NOT EXISTS
                        Organizations (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL,
                            SlugTenant TEXT NOT NULL
                            );
                        CREATE TABLE IF NOT EXISTS
                        Users (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Email TEXT NOT NULL,
                            Password TEXT NOT NULL,
                            OrganizationId INTEGER
                        );";

            await connection.ExecuteAsync(sql);
        }

        public async Task CreateTenantDb(string slugTenant)
        {

            var baseString = _configuration.GetConnectionString("TenantProductsBase");
            var connectionString = string.Format(baseString, slugTenant);

            var connection = new SqliteConnection(connectionString);

            var script = await File.ReadAllTextAsync("Src/Infrastructure/Utils/Scripts/create_new_tenant_db.sql");

            await connection.ExecuteAsync(script);
        }
    }
}