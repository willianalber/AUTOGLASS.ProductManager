using AUTOGLASS.ProductManager.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace AUTOGLASS.ProductManager.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        [HttpPost]
        public void Post([FromBody] SupplierRequest supplierRequest)
        {

        }
    }
}
