using Dapper;
using Microsoft.Data.Sqlite;
using PruebaTecnicaMultitenant.Src.Domain.Entities;
using PruebaTecnicaMultitenant.Src.Domain.Services;

namespace PruebaTecnicaMultitenant.Src.Infrastructure.Services
{
    public class OrganizationsService : IOrganizationsService
    {
        private readonly string? _connectionString;

        public OrganizationsService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MainConnection");
        }

        public async Task<int> Create(Organization organization)
        {
            using var connection = new SqliteConnection(_connectionString);

            var sql = @"INSERT INTO Organizations (
                        Name,
                        SlugTenant
                        )
                      VALUES (
                        @name,
                        @slugTenant 
                        );
                        SELECT last_insert_rowid();
                        ";

            return await connection.ExecuteScalarAsync<int>(
                sql, 
                new { 
                    name = organization.Name, 
                    slugTenant = organization.SlugTenant 
            });
        }
    }

}