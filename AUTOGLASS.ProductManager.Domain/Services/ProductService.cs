using AUTOGLASS.ProductManager.Application.Dtos;
using AUTOGLASS.ProductManager.Domain.Dtos;
using AUTOGLASS.ProductManager.Domain.Entities;
using AUTOGLASS.ProductManager.Domain.Interfaces;

namespace AUTOGLASS.ProductManager.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository repository,
            ISupplierRepository supplierRepository)
        {
            _productRepository = repository;
            _supplierRepository = supplierRepository;
        }

        public async Task Create(ProductDto productDto)
        {
            var supplier = await _supplierRepository.GetByCnpj(productDto.SupplierDto.Cnpj);
            if (supplier == null)
            {
                supplier = new Supplier(productDto.SupplierDto);
            }

            var product = new Product(productDto, supplier);
            Validate(product);

            await _productRepository.Create(product);
        }

        private void Validate(Product product)
        {
            var validationResult = product.IsValid();
            if (validationResult != null && validationResult.Errors.Any())
            {
                throw new Exception(validationResult.Errors.First().ErrorMessage);
            }
        }

        public async Task Delete(long productId)
        {
            var product = await _productRepository.GetById(productId);
            product.Disable();
            await _productRepository.Update(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            var products = await _productRepository.GetAll();
            return products.Select(x => BuildProductDto(x));
        }        

        public async Task<ProductDto> GetById(long productId)
        {
            var product = await _productRepository.GetById(productId);
            return BuildProductDto(product);
        }

        public async Task Update(ProductDto productDto)
        {
            var product = await _productRepository.GetById(productDto.Id);
            product.Update(productDto);
        }

        private ProductDto BuildProductDto(Product product)
        {
            return new ProductDto()
            {
                Id = product.Id,
                CreateDate = product.CreateDate,
                Description = product.Description,
                ExpirationDate = product.ExpirationDate,
                SupplierDto = new SupplierDto
                {
                    Description = product.Supplier.Description,
                    Cnpj = product.Supplier.Cnpj
                }
            };
        }
    }
}
