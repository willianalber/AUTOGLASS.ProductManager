using AUTOGLASS.ProductManager.Domain.Entities;
using AUTOGLASS.ProductManager.Domain.Interfaces;
using AUTOGLASS.ProductManager.Domain.Services;
using AUTOGLASS.ProductManager.Infra.Contex;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUTOGLASS.ProductManager.Infra.Repositories
{
    public class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(ProductManagerContext dbContext) : base(dbContext)
        {
        }

        public async Task<Supplier> GetByCnpj(string cnpj)
        {
            return await _dbContext.Set<Supplier>()
                .FirstOrDefaultAsync(x => x.Cnpj == cnpj);
        }
    }
}
