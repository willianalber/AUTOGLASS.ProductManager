using AUTOGLASS.ProductManager.Api.Models.Supplier;
using AUTOGLASS.ProductManager.Domain.Dtos;
using AUTOGLASS.ProductManager.Domain.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AUTOGLASS.ProductManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService, IMapper mapper)
        {
            _mapper = mapper;
            _supplierService = supplierService;
        }


        /// <summary>
        /// Create suppler
        /// </summary>
        /// <param name="supplierRequest">Model to create supplier</param>
        [HttpPost]
        public async Task Create([FromBody] SupplierRequest supplierRequest)
        {
            var supplierDto = _mapper.Map<SupplierDto>(supplierRequest);
            await _supplierService.Create(supplierDto);
        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="supplierId">Supplier id</param>
        /// <param name="supplierRequest">Model to update product</param>
        [HttpPut]
        public async Task Update([FromQuery] long supplierId, [FromBody] SupplierRequest supplierRequest)
        {
            var supplierDto = _mapper.Map<SupplierDto>(supplierRequest);
            supplierDto.Id = supplierId;
            await _supplierService.Update(supplierDto);
        }

        /// <summary>
        /// Get all suppliers
        /// </summary>
        [HttpGet]
        public async Task<IEnumerable<SupplierResponse>> GetAll()
        {
            var suppliersDtos = await _supplierService.GetAll();
            return suppliersDtos.Select(x => _mapper.Map<SupplierResponse>(x));
        }
    }
}
