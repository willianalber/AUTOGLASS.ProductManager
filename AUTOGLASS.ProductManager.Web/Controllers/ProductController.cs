using AUTOGLASS.ProductManager.Api.Models.Product;
using AUTOGLASS.ProductManager.Application.Dtos;
using AUTOGLASS.ProductManager.Domain.Dtos;
using AUTOGLASS.ProductManager.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace AUTOGLASS.ProductManager.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task Create([FromBody] ProductRequest request)
        {
            if (request == null)
            {
                return;
            }

            var productDto = BuildProductDto(request);
            await _productService.Create(productDto);
        }

        private static ProductDto BuildProductDto(ProductRequest request)
        {
            return new ProductDto()
            {
                CreateDate = request.CreateDate,
                Description = request.Description,
                ExpirationDate = request.ExpirationDate,
                SupplierDto = new SupplierDto()
                {
                    Cnpj = request.Supplier.Cnpj,
                    Description = request.Supplier.Description
                }
            };
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
