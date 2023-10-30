using AUTOGLASS.ProductManager.Domain.Validators;
using AUTOGLASS.ProductManager.Tests.Builders;
using FluentAssertions;
using Xunit;

namespace AUTOGLASS.ProductManager.Tests.Unit.Validator
{
    public class ProductValidatorTests
    {
        [Fact]
        public void Should_return_erros_when_expiration_date_before_create_date()
        {
            //arrange
            var product = new ProductBuilder()
                .WithCreateDate(DateTime.Now.AddDays(1))
                .WithExpirationDate(DateTime.Now)
                .Build();

            var validator = new ProductValidator();

            //action
            var response = validator.Validate(product);

            //assert
            response.Errors.Should().HaveCount(1);
            response.Errors.FirstOrDefault()?.ErrorMessage.Should().Contain("A data de fabricação deve ser anterior à data de validade.");
        }

        [Fact]
        public void Should_return_erros_when_description_by_product_not_informed()
        {
            //arrange
            var product = new ProductBuilder()
                .WithDescription(null)
                .Build();

            var validator = new ProductValidator();

            //action
            var response = validator.Validate(product);

            //assert
            response.Errors.Should().HaveCount(1);
            response.Errors.FirstOrDefault()?.ErrorMessage.Should().Contain("A descrição do produto deve ser informado.");
        }

        [Fact]
        public void Should_return_erros_when_supplier_by_product_not_informed()
        {
            //arrange
            var product = new ProductBuilder()
                .WithSupplierId(0)
                .Build();

            var validator = new ProductValidator();

            //action
            var response = validator.Validate(product);

            //assert
            response.Errors.Should().HaveCount(1);
            response.Errors.FirstOrDefault()?.ErrorMessage.Should().Contain("Deve ser informado um fornecedor valido!");
        }

        [Fact]
        public void Should_not_return_erros_when_create_product_valid()
        {
            //arrange
            var product = new ProductBuilder()
                .Build();

            var validator = new ProductValidator();

            //action
            var response = validator.Validate(product);

            //assert
            response.Errors.Should().BeEmpty();
        }
    }
}
