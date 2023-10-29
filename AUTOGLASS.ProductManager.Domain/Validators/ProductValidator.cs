using AUTOGLASS.ProductManager.Domain.Entities;
using FluentValidation;

namespace AUTOGLASS.ProductManager.Domain.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(product => product.CreateDate)
                .LessThan(product => product.ExpirationDate)
                .WithMessage("A data de fabricação deve ser anterior à data de validade.");
        }
    }
}
