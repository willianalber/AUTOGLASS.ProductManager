using AUTOGLASS.ProductManager.Application.Dtos;

namespace AUTOGLASS.ProductManager.Domain.Services
{
    public interface IProductService
    {
        public Task Create(ProductDto productDto);
        public Task Update(ProductDto productDto);
        public Task Delete(long productId);       
        public Task<IEnumerable<ProductDto>> GetAll();
        public Task<ProductDto> GetById(long productId);
    }
}
