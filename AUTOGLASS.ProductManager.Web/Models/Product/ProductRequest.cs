using AUTOGLASS.ProductManager.Api.Models.Supplier;

namespace AUTOGLASS.ProductManager.Api.Models.Product
{
    public class ProductRequest
    {
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public SupplierRequest Supplier { get; set; }
    }
}
