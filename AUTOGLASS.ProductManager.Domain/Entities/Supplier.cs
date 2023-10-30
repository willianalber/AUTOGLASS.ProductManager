using AUTOGLASS.ProductManager.Domain.Dtos;

namespace AUTOGLASS.ProductManager.Domain.Entities
{
    public class Supplier : EntityBase
    {
        protected Supplier() {}

        public Supplier(SupplierDto supplierDto)
        {
            Description = supplierDto.Description;
            Cnpj = supplierDto.Cnpj;
        }

        public string Description { get; private set; }
        public string Cnpj { get; private set; }

        public void Update(SupplierDto supplierDto)
        {
            Description = supplierDto.Description;
            Cnpj = supplierDto.Cnpj;
        }
    }
}
