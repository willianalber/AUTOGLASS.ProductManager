using AUTOGLASS.ProductManager.Application.Dtos;
using AUTOGLASS.ProductManager.Domain.Entities;
using AUTOGLASS.ProductManager.Tests.Builders;
using FluentAssertions;
using Xunit;

namespace AUTOGLASS.ProductManager.Tests.Unit.Entities
{
    public class ProductTests
    {
        [Fact]
        public void Should_crete_product_enabled()
        {
            //arrange
            var supplier = new SupplierBuilder()
                .Build();

            var dto = new ProductDto
            {
                CreateDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddDays(1),
                Description = "teste"
            };

            //action
            var product = new Product(dto, supplier);
;
            //assert
            product.Status.Should().BeTrue();
            product.Description.Should().Be(dto.Description);
            product.CreateDate.Should().Be(dto.CreateDate);
            product.ExpirationDate.Should().Be(dto.ExpirationDate);
            product.Supplier.Should().Be(supplier);
        }

        [Fact]
        public void Should_disable_product()
        {
            //arrange
            var product = new ProductBuilder()
                .Build();

            //action
            product.Disable();

            //assert
            product.Status.Should().BeFalse();
        }

        [Fact]
        public void Should_active_product()
        {
            //arrange
            var product = new ProductBuilder()
                .Build();

            //action
            product.Active();

            //assert
            product.Status.Should().BeTrue();
        }

        [Fact]
        public void Should_return_error_when_create_date_is_after_expiration_date()
        {
            //arrange
            var product = new ProductBuilder()
                .WithCreateDate(DateTime.Now.AddDays(1))
                .WithExpirationDate(DateTime.Now)
                .Build();

            //action
            var response = product.IsValid();

            //assert
            response.Errors.Should().HaveCount(1);
            response.Errors.Should().Contain(x => x.ErrorMessage == "A data de fabricação deve ser anterior à data de validade.");
        }
    }
}
