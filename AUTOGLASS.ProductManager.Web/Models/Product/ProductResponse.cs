namespace AUTOGLASS.ProductManager.Api.Models.Product
{
    public class ProductResponse
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Supplier { get; set; }
        public string Cnpj { get; set; }

    }
}
