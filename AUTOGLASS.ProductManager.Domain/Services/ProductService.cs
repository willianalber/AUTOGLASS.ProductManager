using AUTOGLASS.ProductManager.Application.Dtos;
using AUTOGLASS.ProductManager.Domain.Dtos;
using AUTOGLASS.ProductManager.Domain.Entities;
using AUTOGLASS.ProductManager.Domain.Filters;
using AUTOGLASS.ProductManager.Domain.Interfaces;

namespace AUTOGLASS.ProductManager.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Create(ProductDto productDto)
        {            
            var product = new Product(productDto);
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

            if (product is null)
                return;

            product.Disable();

            await _productRepository.Update(product);
        }

        public async Task<PaginatedDto<ProductDto>> GetByFilterPaginated(ProductFilter filter)
        {
            var productsFiltered = await _productRepository.GetByFilter(filter);
             

            return new PaginatedDto<ProductDto>() 
            {
                Items = BuildProductsDtos(productsFiltered.Items),
                ItemsByPage = productsFiltered.ItemsByPage,
                PageIndex = productsFiltered.PageIndex,
                TotalItems = productsFiltered.TotalItems
            };
        }        

        public async Task<ProductDto> GetById(long productId)
        {
            var product = await _productRepository.GetById(productId);
            if (product is null) 
                return null;

            return BuildProductDto(product);
        }

        public async Task Update(ProductDto productDto)
        {
            var product = await _productRepository.GetById(productDto.Id);

            if (product is null)
            {
                return;
            }

            product.Update(productDto);
            Validate(product);

            await _productRepository.Update(product);
        }

        private ProductDto BuildProductDto(Product product)
        {
            return new ProductDto()
            {
                Id = product.Id,
                CreateDate = product.CreateDate,
                Description = product.Description,
                ExpirationDate = product.ExpirationDate,
                Supplier = product.Supplier,
            };
        }

        private IEnumerable<ProductDto> BuildProductsDtos(IEnumerable<Product> products)
        {
            return products.Select(x => BuildProductDto(x));
        }
    }
}
