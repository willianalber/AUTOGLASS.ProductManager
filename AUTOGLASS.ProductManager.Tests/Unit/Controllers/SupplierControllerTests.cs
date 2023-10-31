using AUTOGLASS.ProductManager.Api.Models.Supplier;
using AUTOGLASS.ProductManager.Domain.Dtos;
using AUTOGLASS.ProductManager.Domain.Services;
using AUTOGLASS.ProductManager.Web.Controllers;
using NSubstitute;
using Xunit;

namespace AUTOGLASS.ProductManager.Tests.Unit.Controllers
{
    public class SupplierControllerTests
    {
        private readonly ISupplierService _supplierService;
        private readonly SupplierController _supplierController;

        public SupplierControllerTests()
        {
            _supplierService = Substitute.For<ISupplierService>();
            _supplierController = new SupplierController(_supplierService);
        }

        [Fact]
        public async Task Should_call_service_to_create_supllier()
        {
            //arrange
            var supplierRequest = new SupplierRequest { Description = "Fornecedor ABC", Cnpj = "12345678901234" };

            //action
            await _supplierController.Create(supplierRequest);

            //assert
            await _supplierService
                .Received(1)
                .Create(Arg.Is<SupplierDto>(x => x.Cnpj == supplierRequest.Cnpj 
                    && x.Description == supplierRequest.Description));
        }

        [Fact]
        public async Task Shoud_service_to_update_supplier()
        {
            //arrange
            var supplierId = 1;
            var supplierRequest = new SupplierRequest { Description = "Fornecedor ABC", Cnpj = "12345678901234" };

            //act
            await _supplierController.Update(supplierId, supplierRequest);

            //assert
            await _supplierService.Received(1).Update(Arg.Is<SupplierDto>(x =>
                x.Id == supplierId 
                && x.Cnpj == supplierRequest.Cnpj 
                && x.Description == supplierRequest.Description));
        }

        [Fact]
        public async Task Should_call_service_to_get_all_suppliers_and_map_to_response()
        {
            //arrange
            var suppliersDtos = new List<SupplierDto>
            {
                new SupplierDto { Id = 1, Description = "Fornecedor 1", Cnpj = "123" },
                new SupplierDto { Id = 2, Description = "Fornecedor 2", Cnpj = "456" }
            };

            _supplierService.GetAll().Returns(suppliersDtos);

            //act
            var result = await _supplierController.GetAll();

            //assert
            await _supplierService.Received(1).GetAll(); 

            Assert.NotNull(result);
            Assert.Equal(suppliersDtos.Count(), result.Count());

            foreach (var (expectedDto, actualResponse) in suppliersDtos.Zip(result, (dto, response) => (dto, response)))
            {
                Assert.Equal(expectedDto.Id, actualResponse.Id);
                Assert.Equal(expectedDto.Description, actualResponse.Description);
                Assert.Equal(expectedDto.Cnpj, actualResponse.Cnpj);
            }
        }
    }
}
