using AUTOGLASS.ProductManager.Domain.Entities;

namespace AUTOGLASS.ProductManager.Tests.Builders
{
    public class SupplierBuilder
    {
        public Supplier Build()
        {
            return new Supplier("Test supplier", "111.111.111-11");
        }
    }
}
