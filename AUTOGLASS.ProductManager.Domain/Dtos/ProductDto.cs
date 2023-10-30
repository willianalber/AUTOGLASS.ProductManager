using AUTOGLASS.ProductManager.Domain.Entities;

namespace AUTOGLASS.ProductManager.Application.Dtos
{
    public class ProductDto
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public long SupplierId { get; set; }
        public Supplier Supplier { get; set; }        
    } 
}
