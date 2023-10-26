using PruebaTecnicaMultitenant.Src.Domain.Entities;
using PruebaTecnicaMultitenant.Src.Domain.Services;

namespace PruebaTecnicaMultitenant.Src.Application.UseCases
{
    public class ProductsUseCases
    {
        private readonly IProductsService _productsService;
        public ProductsUseCases(IProductsService productsService)
        {
            _productsService = productsService;
        }

        public async Task<int> Create(Product product)
        {
            return await _productsService.Create(product);
        }

        public async Task Delete(int id)
        {
            await _productsService.Delete(id);
        }

        public async Task<Product?> Get(int id)
        {
            return await _productsService.Get(id);
        }

        public async Task Update(int id, Product product)
        {
            await _productsService.Update(id, product);
        }
    }
}