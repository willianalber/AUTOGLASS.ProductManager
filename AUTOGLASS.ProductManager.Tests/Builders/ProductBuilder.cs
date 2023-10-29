using AUTOGLASS.ProductManager.Application.Dtos;
using AUTOGLASS.ProductManager.Domain.Entities;

namespace AUTOGLASS.ProductManager.Tests.Builders
{
    public class ProductBuilder 
    {
        private long _id = 1;
        private string _description = "Product test";
        private DateTime _createDate = DateTime.Now;
        private DateTime _expirationDate = DateTime.Now.AddDays(1);
        private Supplier _supplier = new SupplierBuilder().Build();

        public ProductBuilder WithDescription(string description) 
        { 
            _description = description;
            return this;
        }

        public ProductBuilder WithCreateDate(DateTime createDate)
        {
            _createDate = createDate;
            return this;
        }

        public ProductBuilder WithExpirationDate(DateTime expirationDate)
        {
            _expirationDate = expirationDate;
            return this;
        }

        public ProductBuilder WithSupplier(Supplier supplier)
        {
            _supplier = supplier;
            return this;
        }

        public Product Build()
        {
            var dto = new ProductDto
            {
                CreateDate = _createDate,
                ExpirationDate = _expirationDate,
                Description = _description
            };
            return new Product(dto, _supplier);
        }
    }
}
