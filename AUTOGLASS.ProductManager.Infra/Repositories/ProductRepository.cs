using AUTOGLASS.ProductManager.Domain.Dtos;
using AUTOGLASS.ProductManager.Domain.Entities;
using AUTOGLASS.ProductManager.Domain.Filters;
using AUTOGLASS.ProductManager.Domain.Interfaces;
using AUTOGLASS.ProductManager.Infra.Contex;
using Microsoft.EntityFrameworkCore;

namespace AUTOGLASS.ProductManager.Infra.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ProductManagerContext dbContext) : base(dbContext)
        {
        }

        public async Task<PaginatedDto<Product>> GetByFilter(ProductFilter filter)
        {
            var query = _dbContext.Set<Product>()
                .Include(x => x.Supplier)
                .Where(x => x.Status)
                .AsQueryable();

            query = ApplyFilter(filter, query);
            var totalItems = query.Count();

            query = ApplyPagination(filter, query);
            var products = await query.ToListAsync();

            return new PaginatedDto<Product>
            {
                Items = products,
                TotalItems = totalItems,
                ItemsByPage = filter.ItemsByPage,
                PageIndex = filter.PageIndex
            };
        }        

        private static IQueryable<Product> ApplyPagination(ProductFilter filter, IQueryable<Product> query)
        {
            return query.Skip((filter.PageIndex - 1) * filter.ItemsByPage)
                            .Take(filter.ItemsByPage);
        }

        private IQueryable<Product> ApplyFilter(ProductFilter filter, IQueryable<Product> query)
        {
            if (filter.Status.HasValue)
                query = query.Where(x => x.Status == filter.Status);
            if (filter.Description != null)
                query = query.Where(x => x.Description.Contains(filter.Description));
            if (filter.CreateDate.HasValue)
                query = query.Where(x => x.CreateDate.Date == filter.CreateDate);
            if (filter.ExpirationDate.HasValue)
                query = query.Where(x => x.ExpirationDate.Date == filter.ExpirationDate);
            if (filter.Supplier != null)
                query = query.Where(x => x.Supplier.Description.Contains(filter.Supplier));
            if (filter.Cnpj != null)
                query = query.Where(x => x.Supplier.Cnpj.Contains(filter.Cnpj));
            return query;
        }

        public override async Task<Product> GetById(long productId)
        {
            return await _dbContext.Set<Product>()
                .Include(x => x.Supplier)
                .FirstOrDefaultAsync(x => x.Id == productId);
        }
    }
}
