using AUTOGLASS.ProductManager.Application.Dtos;
using AUTOGLASS.ProductManager.Domain.Dtos;
using AUTOGLASS.ProductManager.Domain.Filters;

namespace AUTOGLASS.ProductManager.Domain.Services
{
    public interface IProductService
    {
        public Task Create(ProductDto productDto);
        public Task Update(ProductDto productDto);
        public Task Delete(long productId);       
        public Task<PaginatedDto<ProductDto>> GetByFilterPaginated(ProductFilter filter);
        public Task<ProductDto> GetById(long productId);
    }
}
