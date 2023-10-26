using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaMultitenant.Src.Application.UseCases;
using PruebaTecnicaMultitenant.Src.Domain.Entities;
using PruebaTecnicaMultitenant.Src.Infrastructure;
using PruebaTecnicaMultitenant.Src.Infrastructure.Utils;
using PruebaTecnicaMultitenant.Src.Infrastructure.Utils.Constants;

namespace PruebaTecnicaMultitenant.Src.API.Controllers
{
    [Authorize]
    [RequiresClaim(ClaimNames.OrganizationSlug)]
    [ApiController]
    [Route("{slugTenant}/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsUseCases _productsUseCases;
        public ProductsController(ProductsUseCases productsUseCases)
        {
            _productsUseCases = productsUseCases;
        }

        //POST http://host:puerto/<slug>/Products - Requiere header de authorization con token valido
        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create(CreateProductDto createProductDto)
        {

            try
            {                
                var product = new Product()
                {
                    Name = createProductDto.Name,
                };

                product.Id = await _productsUseCases.Create(product);
                
                return Ok(product.AsDto());
            }
            catch (Exception)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError);
            }
        }

         //DELETE http://host:puerto/<slug>/Products?=<id> - Requiere header de authorization con token valido
        [HttpDelete()]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _productsUseCases.Delete(id);
                return Ok();
            }
            catch (Exception)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError);
            }
        }
        
        //GET http://host:puerto/<slug>/Products?=<id> - Requiere header de authorization con token valido
        [HttpGet()]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            try
            {                
                var product = await _productsUseCases.Get(id);
                if(product == null)
                    return NotFound();

                return product.AsDto();
            }
            catch (Exception)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        //PUT http://host:puerto/<slug>/Products?=<id> - Requiere header de authorization con token valido
        [HttpPut()]
        public async Task<ActionResult<ProductDto>> Update(int id, CreateProductDto createProductDto)
        {
            try
            {
                var product = new Product()
                {
                    Id = id,
                    Name = createProductDto.Name,
                };

                await _productsUseCases.Update(id, product);         

                return product.AsDto();
            }
            catch (Exception)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}