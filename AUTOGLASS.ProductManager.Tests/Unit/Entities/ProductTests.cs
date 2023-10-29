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
                Supplier = supplier,
                Description = "teste"
            };

            //action
            var product = new Product(dto);
;
            //assert
            product.Enabled.Should().BeTrue();
            product.Description.Should().Be(dto.Description);
            product.CreateDate.Should().Be(dto.CreateDate);
            product.ExpirationDate.Should().Be(dto.ExpirationDate);
            product.Supplier.Should().Be(dto.Supplier);
        }
    }
}
