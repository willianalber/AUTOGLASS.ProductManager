namespace AUTOGLASS.ProductManager.Domain.Dtos
{
    public class PaginatedDto<P>
    {
        public int TotalItems { get; set; }
        public int ItemsByPage { get; set; }
        public int PageIndex { get; set; }
        public IEnumerable<P> Items { get; set; }
    }
}
