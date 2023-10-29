using AUTOGLASS.ProductManager.Application.Dtos;

namespace AUTOGLASS.ProductManager.Domain.Entities
{
    public class Product : EntityBase
    {
        public Product(ProductDto dto)
        {
            Description = dto.Description;
            CreateDate = dto.CreateDate;
            ExpirationDate = dto.ExpirationDate;
            SupplierId = dto.Supplier.Id;
            Supplier = dto.Supplier;
            Enabled = true;
        }

        public string Description { get; private set; }
        public bool Enabled { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public long SupplierId { get; private set; }
        public Supplier Supplier { get; private set; }
    }
}
