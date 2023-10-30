using AUTOGLASS.ProductManager.Domain.Dtos;
using AUTOGLASS.ProductManager.Domain.Entities;
using AUTOGLASS.ProductManager.Domain.Interfaces;
using AUTOGLASS.ProductManager.Domain.Services;
using AUTOGLASS.ProductManager.Tests.Builders;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace AUTOGLASS.ProductManager.Tests.Unit.Services
{
    public class SupplierServiceTests
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly SupplierService _supplierService;

        public SupplierServiceTests()
        {
            _supplierRepository = Substitute.For<ISupplierRepository>();
            _supplierService = new SupplierService(_supplierRepository);
        }

        [Fact]
        public async Task Shoud_call_repository_to_create_supplier()
        {
            //arrange
            var dto = new SupplierDto()
            {
                Description = "Test",
                Cnpj = "Test"
            };

            //action
            await _supplierService.Create(dto);

            //assert
            await _supplierRepository.Received(1)
                .Create(Arg.Is<Supplier>(x =>
                x.Description == dto.Description
                && x.Cnpj == dto.Cnpj
                ));
        }

        [Fact]
        public async Task Shoud_call_repository_to_get_all_suppliers()
        {
            //arrange
            var supplier = new SupplierBuilder().Build();
            _supplierRepository.GetAll().Returns(new List<Supplier>() { supplier });
            //action
            var result = await _supplierService.GetAll();

            //assert
            result.Should().HaveCount(1);
            result.Should().Contain(x => x.Cnpj == supplier.Cnpj && x.Description == supplier.Description);
        }

        [Fact]
        public async Task Shoud_get_empty_when_not_foud_suppliers()
        {
            //arrange
            _supplierRepository.GetAll().Returns(new List<Supplier>());

            //action
            var result = await _supplierService.GetAll();

            //assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task Shoud_call_repository_to_update_supplier()
        {
            //arrange
            var dto = new SupplierDto()
            {
                Id = 1,
                Description = "Test",
                Cnpj = "Test"
            };

            var supplier = new SupplierBuilder().Build();
            _supplierRepository.GetById(dto.Id).Returns(supplier);

            //action
            await _supplierService.Update(dto);

            //assert
            await _supplierRepository.Received(1)
                .Update(Arg.Is<Supplier>(x =>
                x.Description == dto.Description
                && x.Cnpj == dto.Cnpj
                ));
        }

        [Fact]
        public async Task Shoud_call_repository_to_create_supplier_when_supplier_not_found()
        {
            //arrange
            var dto = new SupplierDto()
            {
                Id = 1,
                Description = "Test",
                Cnpj = "Test"
            };

            //action
            await _supplierService.Update(dto);

            //assert
            await _supplierRepository.Received(1)
                .Create(Arg.Is<Supplier>(x =>
                x.Description == dto.Description
                && x.Cnpj == dto.Cnpj
                ));
        }
    }
}
