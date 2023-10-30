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
            var supplier = new Supplier(supplierDto);
            await _supplierRepository.Create(supplier);
        }

        public async Task<IEnumerable<SupplierDto>> GetAll()
        {
            var suppliers = await _supplierRepository.GetAll();
            return suppliers.Select(x => new SupplierDto
            {
                Id = x.Id,
                Description = x.Description,
                Cnpj = x.Cnpj
            });
        }

        public async Task Update(SupplierDto supplierDto)
        {
            var supplier = await _supplierRepository.GetById(supplierDto.Id);
            if (supplier == null) 
            {
                await Create(supplierDto);
                return;
            }

            supplier.Update(supplierDto);
            await _supplierRepository.Update(supplier);
        }
    }
}
