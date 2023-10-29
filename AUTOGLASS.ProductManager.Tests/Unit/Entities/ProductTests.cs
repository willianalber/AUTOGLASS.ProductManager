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
            var supplier = new SupplierBuilder().Build();
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
            var product = new ProductBuilder().Build();

            //action
            product.Disable();

            //assert
            product.Status.Should().BeFalse();
        }

        [Fact]
        public void Should_active_product()
        {
            //arrange
            var product = new ProductBuilder().Build();

            //action
            product.Active();

            //assert
            product.Status.Should().BeTrue();
        }
    }
}
