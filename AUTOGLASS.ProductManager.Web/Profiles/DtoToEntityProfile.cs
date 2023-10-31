using AUTOGLASS.ProductManager.Api.Models.Product;
using AUTOGLASS.ProductManager.Api.Models.Supplier;
using AUTOGLASS.ProductManager.Application.Dtos;
using AUTOGLASS.ProductManager.Domain.Dtos;
using AUTOGLASS.ProductManager.Domain.Entities;
using AutoMapper;

namespace AUTOGLASS.ProductManager.Api.Profiles
{
    public class DtoToEntityProfile : Profile
    {
        public DtoToEntityProfile()
        {
            CreateMap<SupplierDto, Supplier>();
            CreateMap<Supplier, SupplierDto>();

            CreateMap<SupplierRequest, SupplierDto>();
            CreateMap<SupplierDto, SupplierResponse>();
            
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<ProductDto, ProductResponse>()
                .ForMember(x => x.Supplier, options => options.MapFrom(x => x.Supplier.Description));
        }
    }
}
