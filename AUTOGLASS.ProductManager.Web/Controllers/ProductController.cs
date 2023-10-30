using AUTOGLASS.ProductManager.Api.Models.Errors;
using AUTOGLASS.ProductManager.Api.Models.Product;
using AUTOGLASS.ProductManager.Application.Dtos;
using AUTOGLASS.ProductManager.Domain.Dtos;
using AUTOGLASS.ProductManager.Domain.Filters;
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
        public async Task<ObjectResult> Create([FromBody] ProductRequest request)
        {
            try
            {
                var productDto = BuildProductDto(request);
                await _productService.Create(productDto);
                return StatusCode(200, null);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new ErrorResponse()
                { 
                    Code = "Dados invalidos", 
                    Message = ex.Message 
                });
            }
            
        }        

        [HttpPut("{productId}")]
        public async Task Update(int productId, [FromBody] ProductRequest productRequest)
        {
            if (productRequest == null)
            {
                return;
            }

            var productDto = BuildProductDto(productRequest);
            productDto.Id = productId;

            await _productService.Update(productDto);
        }

        [HttpDelete("{productId}")]
        public async Task Delete([FromRoute] long productId)
        {
            await _productService.Delete(productId);
        }

        [HttpGet]
        public async Task<PaginatedDto<ProductResponse>> GetByFilterPaginated([FromQuery] ProductFilter productFilter)
        {
            var products = await _productService.GetByFilterPaginated(productFilter);
            return new PaginatedDto<ProductResponse>()
            {
                Items = products.Items.Select(x => new ProductResponse()
                {
                    Cnpj = x.Supplier.Cnpj,
                    Supplier = x.Supplier.Description,
                    Description = x.Description,
                    CreateDate = x.CreateDate,
                    ExpirationDate = x.ExpirationDate,
                    Id = x.Id
                }),
                ItemsByPage = products.ItemsByPage,
                PageIndex = products.PageIndex,
                TotalItems = products.TotalItems,
            };
        }

        private static ProductDto BuildProductDto(ProductRequest request)
        {
            return new ProductDto()
            {
                CreateDate = request.CreateDate,
                Description = request.Description,
                ExpirationDate = request.ExpirationDate,
                SupplierId = request.SupplierId
            };
        }
    }
}
