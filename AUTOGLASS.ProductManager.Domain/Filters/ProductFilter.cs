namespace AUTOGLASS.ProductManager.Domain.Filters
{
    public class ProductFilter
    {
        public string? Description { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool? Status { get; set; }
        public string? Supplier { get; set; }
        public string? Cnpj { get; set; }
        public int ItemsByPage { get; set; }
        public int PageIndex { get; set; }
    }
}
