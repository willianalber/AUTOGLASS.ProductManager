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
        public async Task Create([FromBody] SupplierDto supplierRequest)
        {
            await _supplierService.Create(supplierRequest);
        }

        [HttpGet]
        public async Task<IEnumerable<SupplierDto>> GetAll()
        {
            return await _supplierService.GetAll();
        }
    }
}
