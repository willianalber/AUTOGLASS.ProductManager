using AUTOGLASS.ProductManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AUTOGLASS.ProductManager.Infra.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(d => d.Description).IsRequired();
            builder.HasOne(p => p.Supplier).WithMany().HasForeignKey(p => p.SupplierId);
        }
    }
}
