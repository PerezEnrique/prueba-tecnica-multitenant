using PruebaTecnicaMultitenant.Src.Domain.Entities;

namespace PruebaTecnicaMultitenant.Src.Domain.Services
{
    public interface IProductsService
    {
        string ConnectionString {get; set;}
        Task<int> Create(Product product);
        Task Delete(int id);
        Task<Product?> Get(int id);
        Task<int> Update(int id, Product product);
    }
}