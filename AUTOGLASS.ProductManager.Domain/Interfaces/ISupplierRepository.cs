using AUTOGLASS.ProductManager.Domain.Entities;

namespace AUTOGLASS.ProductManager.Domain.Interfaces
{
    public interface ISupplierRepository : IGenericRepository<Supplier>
    {
        Task<Supplier> GetByCnpj(string? cnpj);
    }
}
