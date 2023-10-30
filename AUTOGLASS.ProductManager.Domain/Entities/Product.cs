using AUTOGLASS.ProductManager.Application.Dtos;
using AUTOGLASS.ProductManager.Domain.Validators;
using FluentValidation.Results;

namespace AUTOGLASS.ProductManager.Domain.Entities
{
    public class Product : EntityBase
    {
        protected Product() { }
        public Product(ProductDto dto)
        {
            Description = dto.Description;
            CreateDate = dto.CreateDate;
            ExpirationDate = dto.ExpirationDate;
            SupplierId = dto.SupplierId;
            Supplier = dto.Supplier;
            Status = true;
        }

        public string Description { get; private set; }
        public bool Status { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public long SupplierId { get; private set; }
        public Supplier Supplier { get; private set; }

        public void Active()
        {
            Status = true;
        }

        public void Disable()
        {
            Status = false;
        }

        public void Update(ProductDto productDto)
        {
            Description = productDto.Description;
            CreateDate = productDto.CreateDate;
            ExpirationDate = productDto.ExpirationDate;
            SupplierId = productDto.SupplierId;
        }

        public ValidationResult IsValid() => new ProductValidator().Validate(this);
    }
}
