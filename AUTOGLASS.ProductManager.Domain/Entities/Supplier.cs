namespace AUTOGLASS.ProductManager.Domain.Entities
{
    public class Supplier : EntityBase
    {
        protected Supplier() {}
        public Supplier(string description, string cnpj)
        {
            Description = description;
            Cnpj = cnpj;
        }

        public string Description { get; private set; }
        public string Cnpj { get; private set; }
    }
}
