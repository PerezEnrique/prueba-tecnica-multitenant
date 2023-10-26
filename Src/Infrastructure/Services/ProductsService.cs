using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.Sqlite;
using PruebaTecnicaMultitenant.Src.Domain.Entities;
using PruebaTecnicaMultitenant.Src.Domain.Services;

namespace PruebaTecnicaMultitenant.Src.Infrastructure.Services
{
    public class ProductsService : IProductsService
    {
        public string? ConnectionString { get; set; }

        [Authorize]
        public async Task<int> Create(Product product)
        {
            if(string.IsNullOrWhiteSpace(ConnectionString)) 
                throw new Exception("ConnectionString has not been set");

            using var connection = new SqliteConnection(ConnectionString);

            var sql = "INSERT INTO Products (Name) VALUES (@name); SELECT last_insert_rowid();";

            return await connection.ExecuteScalarAsync<int>(sql, new {name  = product.Name});
        }

        public async Task Delete(int id)
        {
            if(string.IsNullOrWhiteSpace(ConnectionString)) 
                throw new Exception("ConnectionString has not been set");

            using var connection = new SqliteConnection(ConnectionString);

            var sql = "DELETE FROM Products WHERE Id = @id";

            await connection.ExecuteAsync(sql, new { id });
        }

        public async Task<Product?> Get(int id)
        {
            if(string.IsNullOrWhiteSpace(ConnectionString)) 
                throw new Exception("ConnectionString has not been set");

            using var connection = new SqliteConnection(ConnectionString);

            var sql = "SELECT * FROM Products WHERE Id = @id";

            return await connection.QuerySingleOrDefaultAsync<Product>(sql, new { id });
        }

        public async Task Update(int id, Product product)
        {
            if(string.IsNullOrWhiteSpace(ConnectionString)) 
                throw new Exception("ConnectionString has not been set");

            using var connection = new SqliteConnection(ConnectionString);

            var sql = "UPDATE Products SET Name = @name WHERE Id = @id";

            await connection.ExecuteAsync(sql, new { id, name = product.Name });
        }
    }

}