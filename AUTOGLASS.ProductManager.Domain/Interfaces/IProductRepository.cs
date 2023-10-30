using AUTOGLASS.ProductManager.Application.Dtos;
using AUTOGLASS.ProductManager.Domain.Dtos;
using AUTOGLASS.ProductManager.Domain.Entities;
using AUTOGLASS.ProductManager.Domain.Filters;

namespace AUTOGLASS.ProductManager.Domain.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<PaginatedDto<Product>> GetByFilter(ProductFilter filter);
    }
}
