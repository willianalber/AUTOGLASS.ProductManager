namespace AUTOGLASS.ProductManager.Api.Models.Product
{
    public class ProductRequest
    {
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public long SupplierId { get; set; }
    }
}
