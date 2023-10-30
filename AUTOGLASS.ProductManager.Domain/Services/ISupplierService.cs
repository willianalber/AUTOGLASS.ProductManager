using AUTOGLASS.ProductManager.Domain.Dtos;

namespace AUTOGLASS.ProductManager.Domain.Services
{
    public interface ISupplierService
    {
        public Task Create(SupplierDto supplierDto);
        public Task<IEnumerable<SupplierDto>> GetAll();
        public Task Update(SupplierDto supplierDto);
    }
}
