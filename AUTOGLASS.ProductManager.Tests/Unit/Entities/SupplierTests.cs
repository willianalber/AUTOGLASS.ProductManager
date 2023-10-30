using AUTOGLASS.ProductManager.Domain.Dtos;
using AUTOGLASS.ProductManager.Domain.Entities;
using AUTOGLASS.ProductManager.Tests.Builders;
using FluentAssertions;
using Xunit;

namespace AUTOGLASS.ProductManager.Tests.Unit.Entities
{
    public class SupplierTests
    {
        [Fact]
        public void Should_create_supplier()
        {
            //arrange
            var dto = new SupplierDto()
            {
                Cnpj = "12354",
                Description = "description",
            };

            //action
            var entity = new Supplier(dto);

            //assert
            entity.Cnpj.Should().Be(dto.Cnpj);
            entity.Description.Should().Be(dto.Description);
        }

        [Fact]
        public void Should_update_supplier()
        {
            //arrange
            var entity = new SupplierBuilder().Build();
            var dto = new SupplierDto()
            {
                Cnpj = "12354",
                Description = "description",
            };

            //action
            entity.Update(dto);

            //assert
            entity.Cnpj.Should().Be(dto.Cnpj);
            entity.Description.Should().Be(dto.Description);
        }
    }
}
