
using Microsoft.Data.Sqlite;
using PruebaTecnicaMultitenant.Src.Domain.Entities;
using PruebaTecnicaMultitenant.Src.Domain.Services;
using Dapper;

namespace PruebaTecnicaMultitenant.Src.Infrastructure.Services
{
    public class UsersService : IUsersService
    {
        private readonly string? _connectionString;
        
        public UsersService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MainConnection");    
        }

        public async Task<int> Create(User user)
        {
            using var connection = new SqliteConnection(_connectionString);

            var sql = @"INSERT INTO Users (
                    Email,
                    Password,
                    OrganizationId
                    )
                    VALUES (
                    @email,
                    @password,
                    @organizationId 
                    );
                    SELECT last_insert_rowid();
                    ";

            return await connection.ExecuteScalarAsync<int>(
                sql,
                new {
                    email = user.Email,
                    password = user.Password,
                    organizationId = user.OrganizationId
                }
            );
        }

        public async Task<User?> Get(int id)
        {
            using var connection = new SqliteConnection(_connectionString);

            var sql = @"SELECT * FROM Users WHERE Id = @id";

            return await connection.QuerySingleOrDefaultAsync<User>(sql, new { id });
        }


        public async Task<User?> GetByEmail (string email)
        {
            using var connection = new SqliteConnection(_connectionString);

            var sql = @"SELECT * FROM Users WHERE Email = @email";
            
            return await connection.QuerySingleOrDefaultAsync<User>(sql, new { email });
        }
    }
}