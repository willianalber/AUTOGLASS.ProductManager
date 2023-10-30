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
                if (request is null)
                {
                    return StatusCode(StatusCodes.Status200OK, null);
                }

                var productDto = BuildProductDto(request);
                await _productService.Create(productDto);
                return StatusCode(StatusCodes.Status201Created, null);
            }
            catch (Exception ex)
            {
                return BuildError(ex);
            }

        }        

        [HttpPut("{productId}")]
        public async Task<ObjectResult> Update(long productId, [FromBody] ProductRequest productRequest)
        {
            try
            {
                if (productRequest == null)
                {
                    return StatusCode(StatusCodes.Status200OK, null);
                }

                var productDto = BuildProductDto(productRequest);
                productDto.Id = productId;
                await _productService.Update(productDto);

                return StatusCode(StatusCodes.Status201Created, null);
            }
            catch (Exception ex)
            {
                return BuildError(ex);
            }           
            
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

        [HttpGet("{productId}")]
        public async Task<ProductResponse> GetById([FromRoute] long productId)
        {
            var product = await _productService.GetById(productId);
            
            if (product is null)
            {
                return default;
            }

            return new ProductResponse()
            {
                ExpirationDate = product.ExpirationDate,
                Id = product.Id,
                CreateDate = product.CreateDate,
                Description = product.Description,
                Cnpj = product.Supplier.Cnpj,
                Supplier = product.Supplier.Description,
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

        private ObjectResult BuildError(Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponse()
            {
                Code = "Dados invalidos",
                Message = ex.Message
            });
        }
    }
}
