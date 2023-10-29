using AUTOGLASS.ProductManager.Application.Dtos;
using AUTOGLASS.ProductManager.Domain.Dtos;
using AUTOGLASS.ProductManager.Domain.Entities;
using AUTOGLASS.ProductManager.Domain.Interfaces;
using Newtonsoft.Json.Serialization;

namespace AUTOGLASS.ProductManager.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public Task Create(ProductDto productDto)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(long productId)
        {
            var product = await _repository.GetById(productId);
            product.Disable();
            await _repository.Update(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            var products = await _repository.GetAll();
            return products.Select(x => BuildProductDto(x));
        }        

        public async Task<ProductDto> GetById(long productId)
        {
            var product = await _repository.GetById(productId);
            return BuildProductDto(product);
        }

        public async Task Update(ProductDto productDto)
        {
            var product = await _repository.GetById(productDto.Id);
            product.Update(productDto);
        }

        private static ProductDto BuildProductDto(Entities.Product x)
        {
            return new ProductDto()
            {
                Id = x.Id,
                CreateDate = x.CreateDate,
                Description = x.Description,
                ExpirationDate = x.ExpirationDate,
                SupplierDto = new SupplierDto
                {
                    Description = x.Supplier.Description,
                    Cnpj = x.Supplier.Cnpj
                }
            };
        }
    }
}
