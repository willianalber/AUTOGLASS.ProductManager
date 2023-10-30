using AUTOGLASS.ProductManager.Api.Models.Supplier;
using AUTOGLASS.ProductManager.Domain.Dtos;
using AUTOGLASS.ProductManager.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace AUTOGLASS.ProductManager.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpPost]
        public async Task Create([FromBody] SupplierRequest supplierRequest)
        {
            var supplierDto = new SupplierDto() 
            { 
                Description = supplierRequest.Description, 
                Cnpj = supplierRequest.Cnpj 
            };

            await _supplierService.Create(supplierDto);
        }

        [HttpPut]
        public async Task Update([FromQuery] long supplierId, [FromBody] SupplierRequest supplierRequest)
        {
            var supplierDto = new SupplierDto()
            {
                Id = supplierId,
                Description = supplierRequest.Description,
                Cnpj = supplierRequest.Cnpj
            };

            await _supplierService.Update(supplierDto);
        }

        [HttpGet]
        public async Task<IEnumerable<SupplierResponse>> GetAll()
        {
            var suppliersDtos = await _supplierService.GetAll();
            return suppliersDtos.Select(x => new SupplierResponse() 
            {
                Description = x.Description,
                Cnpj = x.Cnpj,
                Id = x.Id
            });
        }
    }
}
