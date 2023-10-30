using AUTOGLASS.ProductManager.Api.Models.Errors;
using AUTOGLASS.ProductManager.Api.Models.Product;
using AUTOGLASS.ProductManager.Application.Dtos;
using AUTOGLASS.ProductManager.Domain.Services;
using AUTOGLASS.ProductManager.Web.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AUTOGLASS.ProductManager.Tests.Unit.Controllers
{
    public class ProductControllerTests
    {
        private readonly IProductService _productService;
        private readonly ProductController _productController;

        public ProductControllerTests()
        {
            _productService = Substitute.For<IProductService>();
            _productController = new ProductController(_productService);
        }

        [Fact]
        public async Task Should_call_service_to_create_product()
        {
            //arrang
            var request = new ProductRequest()
            {
                CreateDate = DateTime.Now,
                Description = "description",
                ExpirationDate = DateTime.Now,
                SupplierId = 1,
            };

            //action
            await _productController.Create(request);

            //assert
            await _productService
                .Received(1)
                .Create(Arg.Is<ProductDto>(x => x.CreateDate == request.CreateDate
                && x.Description == request.Description
                && x.ExpirationDate == request.ExpirationDate
                && x.SupplierId == request.SupplierId
                ));
        }

        [Fact]
        public async Task Should_return_badRequest()
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
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            var errorResponse = result.Value as ErrorResponse;
            Assert.NotNull(errorResponse);
            Assert.Equal("Dados invalidos", errorResponse.Code);
            Assert.Equal("Erro ao criar produto", errorResponse.Message);
        }

        [Fact]
        public async Task Should_call_service_to_update_product()
        {
            //arrang
            var request = new ProductRequest()
            {
                CreateDate = DateTime.Now,
                Description = "description",
                ExpirationDate = DateTime.Now,
                SupplierId = 1,
            };

            long productionId = 50;

            //action
            await _productController.Update(productionId, request);

            //assert
            await _productService
                .Received(1)
                .Update(Arg.Is<ProductDto>(x => x.CreateDate == request.CreateDate
                && x.Description == request.Description
                && x.ExpirationDate == request.ExpirationDate
                && x.SupplierId == request.SupplierId
                && x.Id == productionId
                ));
        }
    }
}
