using AUTOGLASS.ProductManager.Api.Models.Errors;
using AUTOGLASS.ProductManager.Api.Models.Product;
using AUTOGLASS.ProductManager.Application.Dtos;
using AUTOGLASS.ProductManager.Domain.Dtos;
using AUTOGLASS.ProductManager.Domain.Filters;
using AUTOGLASS.ProductManager.Domain.Services;
using AUTOGLASS.ProductManager.Tests.Builders;
using AUTOGLASS.ProductManager.Api.Controllers;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AUTOGLASS.ProductManager.Tests.Unit.Controllers
{
    public class ProductControllerTests
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ProductController _productController;

        public ProductControllerTests()
        {
            _mapper = Substitute.For<IMapper>();
            _productService = Substitute.For<IProductService>();
            _productController = new ProductController(_productService, _mapper);
        }

        [Fact]
        public async Task Should_call_service_to_create_product()
        {
            //arrang
            var request = new ProductRequest();

            var dto = new ProductDto
            {
                CreateDate = DateTime.Now,
                Description = "description",
                ExpirationDate = DateTime.Now,
                SupplierId = 1,
            };

            _mapper.Map<ProductDto>(request).Returns(dto);

            //action
            await _productController.Create(request);

            //assert
            await _productService
                .Received(1)
                .Create(Arg.Is<ProductDto>(x => x.CreateDate == dto.CreateDate
                && x.Description == dto.Description
                && x.ExpirationDate == dto.ExpirationDate
                && x.SupplierId == dto.SupplierId
                ));
        }

        [Fact]
        public async Task Should_return_bad_request_when_create_product_with_error()
        {
            //arrang
            var request = new ProductRequest()
            {
                CreateDate = DateTime.Now,
                Description = "description",
                ExpirationDate = DateTime.Now,
                SupplierId = 1,
            };

            _productService.Create(Arg.Any<ProductDto>())
                .ThrowsAsync(new Exception("Erro ao criar produto"));

            //action
            var result = await _productController.Create(request);

            //assert
            var response = result.As<ObjectResult>();
            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            var errorResponse = response.Value as ErrorResponse;
            Assert.NotNull(errorResponse);
            Assert.Equal("Dados invalidos", errorResponse.Code);
            Assert.Equal("Erro ao criar produto", errorResponse.Message);
        }

        [Fact]
        public async Task Should_call_service_to_update_product()
        {
            //arrang
            var request = new ProductRequest();

            var dto = new ProductDto
            {
                CreateDate = DateTime.Now,
                Description = "description",
                ExpirationDate = DateTime.Now,
                SupplierId = 1,
            };

            _mapper.Map<ProductDto>(request).Returns(dto);

            long productionId = 50;

            //action
            await _productController.Update(productionId, request);

            //assert
            await _productService
                .Received(1)
                .Update(Arg.Is<ProductDto>(x => x.CreateDate == dto.CreateDate
                && x.Description == dto.Description
                && x.ExpirationDate == dto.ExpirationDate
                && x.SupplierId == dto.SupplierId
                && x.Id == productionId
                ));
        }

        [Fact]
        public async Task Should_return_bad_request_when_update_product_with_error()
        {
            //arrang
            var request = new ProductRequest();
            var dto = new ProductDto();

            _mapper.Map<ProductDto>(request).Returns(dto);

            _productService.Update(Arg.Any<ProductDto>())
                .ThrowsAsync(new Exception("Erro ao editar produto"));

            long productId = 10;

            //action
            var result = await _productController.Update(productId, request);

            //assert
            var response = result.As<ObjectResult>();
            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            var errorResponse = response.Value as ErrorResponse;
            Assert.NotNull(errorResponse);
            Assert.Equal("Dados invalidos", errorResponse.Code);
            Assert.Equal("Erro ao editar produto", errorResponse.Message);
        }

        [Fact]
        public async Task Should_call_service_to_delete_product()
        {
            //arrang
            long produtId = 10;

            //action
            await _productController.Delete(produtId);

            //assert
            await _productService.Received(1).Delete(produtId);
        }

        [Fact]
        public async Task Should_call_service_to_get_products_paginated()
        {
            //arrang
            var filter = new ProductFilter();
            var product = new ProductDto()
            {
                Id = 1,
                CreateDate = DateTime.Now,
                Description = "description",
                ExpirationDate= DateTime.Now,
                SupplierId = 1,
                Supplier = new SupplierBuilder().Build()
            };

            var productsPaginated = new PaginatedDto<ProductDto>()
            {
                Items = new List<ProductDto>()
                {
                    product
                },
                ItemsByPage = filter.ItemsByPage,
                PageIndex = filter.PageIndex,
                TotalItems = 10
            };
            _productService.GetByFilterPaginated(filter).Returns(productsPaginated);

            //action
            var response = await _productController.GetByFilterPaginated(filter);

            //assert
            response.TotalItems.Should().Be(productsPaginated.TotalItems);
            response.Items.Should().HaveCount(1);
            response.PageIndex.Should().Be(productsPaginated.PageIndex);
            response.ItemsByPage.Should().Be(productsPaginated.ItemsByPage);
        }

        [Fact]
        public async Task Should_call_service_to_get_product_by_id()
        {
            //arrang
            long productId = 20;
            var product = new ProductDto();
            var productResponse = new ProductResponse()
            {
                Cnpj = "123123",
                CreateDate = DateTime.Now,
                Description = "Description",
                ExpirationDate = DateTime.Now,
                Id = productId,
                Supplier = "TESTE"
            };
            _mapper.Map<ProductResponse>(product).Returns(productResponse);

            _productService.GetById(productId).Returns(product);

            //action
            var response = await _productController.GetById(productId);

            //assert
            response.Cnpj.Should().Be(productResponse.Cnpj);
            response.Supplier.Should().Be(productResponse.Supplier);
            response.CreateDate.Should().Be(productResponse.CreateDate);
            response.ExpirationDate.Should().Be(productResponse.ExpirationDate);
        }

        [Fact]
        public async Task Should_return_default_when_service_to_get_product_by_id_not_found()
        {
            //arrang
            long productId = 20;

            //action
            var response = await _productController.GetById(productId);

            //assert
            response.Should().BeNull();
        }
    }
}
