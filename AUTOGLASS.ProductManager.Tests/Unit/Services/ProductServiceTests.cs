using AUTOGLASS.ProductManager.Application.Dtos;
using AUTOGLASS.ProductManager.Domain.Dtos;
using AUTOGLASS.ProductManager.Domain.Entities;
using AUTOGLASS.ProductManager.Domain.Filters;
using AUTOGLASS.ProductManager.Domain.Interfaces;
using AUTOGLASS.ProductManager.Domain.Services;
using AUTOGLASS.ProductManager.Tests.Builders;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace AUTOGLASS.ProductManager.Tests.Unit.Services
{
    public class ProductServiceTests
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;

        public ProductServiceTests()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _productService = new ProductService(_productRepository);
        }

        [Fact]
        public async Task Shoud_call_repository_to_create_product()
        {
            //arrange
            var dto = new ProductDto()
            {
                CreateDate = DateTime.Now,
                Description = "Test",
                ExpirationDate = DateTime.Now.AddDays(1),
                SupplierId = 1
            };

            //action
            await _productService.Create(dto);

            //assert
            await _productRepository.Received(1)
                .Create(Arg.Is<Product>(x => 
                x.Description == dto.Description
                && x.CreateDate == dto.CreateDate
                && x.ExpirationDate == dto.ExpirationDate
                && x.Status == true
                && x.SupplierId == dto.SupplierId
                ));
        }

        [Fact]
        public async Task Should_not_call_repository_when_create_product_invalid()
        {
            // arrange
            var dto = new ProductDto();

            // action
            var exception = await Record.ExceptionAsync(async () => 
            await _productService.Create(dto));

            //assert
            exception.Message.Should().Be("A data de fabricação deve ser anterior à data de validade.");
            await _productRepository.DidNotReceive().Create(Arg.Any<Product>());
        }

        [Fact]
        public async Task Shoud_call_repository_to_update_when_delete_product()
        {
            //arrange
            var product = new ProductBuilder().Build();
            _productRepository.GetById(product.Id).Returns(product);

            //action
            await _productService.Delete(product.Id);

            //assert
            product.Status.Should().BeFalse();
            await _productRepository.Received(1).Update(product);
        }

        [Fact]
        public async Task Shoud_not_call_repository_to_delete_when_product_not_found()
        {
            //arrange
            long productId = 1;

            //action
            await _productService.Delete(productId);

            //assert
            await _productRepository.DidNotReceive().Update(Arg.Any<Product>());
        }

        [Fact]
        public async Task Shoud_call_repository_to_get_products_paginated()
        {
            //arrange
            var filter = new ProductFilter();
            var product = new ProductBuilder().Build();

            var products = new PaginatedDto<Product>
            {
                ItemsByPage = 1,
                PageIndex = 1,
                TotalItems = 10,
                Items = new List<Product>() { product }
            };

            _productRepository.GetByFilter(filter).Returns(products);

            //action
            var response = await _productService.GetByFilterPaginated(filter);

            //assert
            response.Items.Should().HaveCount(1);
            response.ItemsByPage.Should().Be(products.ItemsByPage);
            response.PageIndex.Should().Be(products.PageIndex);
            response.TotalItems.Should().Be(products.TotalItems);
        }

        [Fact]
        public async Task Shoud_call_repository_to_get_product_by_id()
        {
            //arrange;
            var product = new ProductBuilder().Build();
            _productRepository.GetById(product.Id).Returns(product);

            //action
            var response = await _productService.GetById(product.Id);

            //assert
            response.Should().NotBeNull();
        }

        [Fact]
        public async Task Shoud_return_null_when_get_product_by_id_not_found()
        {
            //arrange;
            long productId = 1;

            //action
            var response = await _productService.GetById(productId);

            //assert
            response.Should().BeNull();
        }

        [Fact]
        public async Task Shoud_call_repository_to_update_product()
        {
            //arrange
            var dto = new ProductDto()
            {
                CreateDate = DateTime.Now,
                Description = "Test",
                ExpirationDate = DateTime.Now.AddDays(1),
                SupplierId = 1
            };

            var product = new ProductBuilder().Build();
            _productRepository.GetById(dto.Id).Returns(product);

            //action
            await _productService.Update(dto);

            //assert
            await _productRepository.Received(1)
                .Update(Arg.Is<Product>(x =>
                x.Description == dto.Description
                && x.CreateDate == dto.CreateDate
                && x.ExpirationDate == dto.ExpirationDate
                && x.Status == true
                && x.SupplierId == dto.SupplierId
                ));
        }

        [Fact]
        public async Task Shoud_not_call_repository_to_update_when_product_not_found()
        {
            //arrange
            var dto = new ProductDto()
            {
                CreateDate = DateTime.Now,
                Description = "Test",
                ExpirationDate = DateTime.Now.AddDays(1),
                SupplierId = 1
            };

            //action
            await _productService.Update(dto);

            //assert
            await _productRepository.DidNotReceive().Update(Arg.Any<Product>());
        }

        [Fact]
        public async Task Should_not_call_repository_when_update_product_invalid()
        {
            // arrange
            var dto = new ProductDto();
            var product = new ProductBuilder().Build();
            _productRepository.GetById(dto.Id).Returns(product);

            // action
            var exception = await Record.ExceptionAsync(async () =>
            await _productService.Update(dto));

            //assert
            exception.Message.Should().Be("A data de fabricação deve ser anterior à data de validade.");
            await _productRepository.DidNotReceive().Update(Arg.Any<Product>());
        }
    }
}
