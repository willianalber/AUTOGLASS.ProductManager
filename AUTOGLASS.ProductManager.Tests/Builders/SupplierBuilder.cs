using AUTOGLASS.ProductManager.Domain.Dtos;
using AUTOGLASS.ProductManager.Domain.Entities;

namespace AUTOGLASS.ProductManager.Tests.Builders
{
    public class SupplierBuilder
    {
        private string _description = "teste_desc";
        private string _cnpj = "teste_cnpj";

        public Supplier Build()
        {
            var supplierDto = new SupplierDto()
            {
                Cnpj = _cnpj,
                Description = _description
            };

            return new Supplier(supplierDto);
        }
    }
}
