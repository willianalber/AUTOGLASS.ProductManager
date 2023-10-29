using AUTOGLASS.ProductManager.Domain.Dtos;
using AUTOGLASS.ProductManager.Domain.Entities;
using AUTOGLASS.ProductManager.Domain.Interfaces;

namespace AUTOGLASS.ProductManager.Domain.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task Create(SupplierDto supplierDto)
        {
            var supplier = new Supplier(supplierDto.Description, supplierDto.Cnpj);
            await _supplierRepository.Create(supplier);
        }

        public async Task<IEnumerable<SupplierDto>> GetAll()
        {
            var suppliers = await _supplierRepository.GetAll();
            return suppliers.Select(x => new SupplierDto
            {
                Description = x.Description,
                Cnpj = x.Cnpj
            });
        }
    }
}
