using DataAccess.Data;
using DataAccess.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using PremiseContractsService.DTOs;
using PremiseContractsTests.Extensions;

namespace PremiseContractsTests;

[TestFixture]
public class MyApiTests
{
    private HttpClient _client;
    private WebApplicationFactory<Program> _factory;

    [SetUp]
    public void SetUp()
    {
        _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType ==
                            typeof(DbContextOptions<PremiseContractsContext>));

                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }
                    services.AddDbContext<PremiseContractsContext>(options => options.UseInMemoryDatabase("TestDb"));
                });
            });
        _client = _factory.CreateClient();
        _client.DefaultRequestHeaders.Add("XApiKey", "643fc18bf655df20be800fbf3c9608e2244d0673c1860b1622611a75b1e1ed2a");
    }

    [TearDown]
    public void Teardown()
    {
        _client.Dispose();
        _factory.Dispose();
    }

    [Test]
    public async Task TaskGetContractsAsync_ShouldReturnListOfContracts()
    {
        // Arrange
        using (var scope = _factory.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<PremiseContractsContext>();
            dbContext.Contracts.Add(new Contract
            {
                PremiseCode = "Test1",
                EquipmentCode = "Test2",
                Quantity = 5,
                Premise = new Premise { Code = "Test1", Name = "Test11", Area = 500 },
                Equipment = new Equipment { Code = "Test2", Name = "Test22", Area = 50 }
            });
            dbContext.SaveChanges();
        }

        // Act
        var response = await _client.GetAsync("/Contracts");

        // Assert
        response.Should().Be200Ok().And.NotBeNull();
    }

    [Test]
    public async Task TaskCreateContractAsync_ShouldCreateContract()
    {
        // Arrange
        using (var scope = _factory.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<PremiseContractsContext>();
            dbContext.Premises.Add(new Premise { Code = "Test711111", Name = "Test71", Area = 500 });
            dbContext.Equipment.Add(new Equipment { Code = "Test811111", Name = "Test72", Area = 50 });
            dbContext.SaveChanges();
        }

        var contract = new ContractCreateDto
        {
            PremiseCode = "Test711111",
            EquipmentCode = "Test811111",
            Quantity = 1
        };

        // Act
        var response = await _client.PostJsonAsync("/Contracts", contract);

        // Assert
        response.Should().Be201Created();
    }

    [Test]
    public async Task TaskCreateContractAsync_ShouldThrowAnExceptionIfAreaIsNotEnough()
    {
        // Arrange
        using (var scope = _factory.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<PremiseContractsContext>();
            dbContext.Premises.Add(new Premise { Code = "Test311111", Name = "Test31", Area = 500 });
            dbContext.Equipment.Add(new Equipment { Code = "Test411111", Name = "Test42", Area = 50 });
            dbContext.SaveChanges();
        }

        var contract = new ContractCreateDto
        {
            PremiseCode = "Test311111",
            EquipmentCode = "Test411111",
            Quantity = 20
        };

        // Act
        var response = await _client.PostJsonAsync("/Contracts", contract);

        // Assert
        response.Should()
            .HaveErrorMessage("Not enough free premise's area for this equipment quantity")
            .And.Be500InternalServerError();
    }

    [Test]
    public async Task TaskCreateContractAsync_ShouldThrowAnExceptionIfPremiseOrEquipmentDoesNotExist()
    {
        // Arrange
        using (var scope = _factory.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<PremiseContractsContext>();
            dbContext.Premises.Add(new Premise { Code = "Test5", Name = "Test61", Area = 500 });
            dbContext.Equipment.Add(new Equipment { Code = "Test6", Name = "Test62", Area = 50 });
            dbContext.SaveChanges();
        }

        var contract = new ContractCreateDto
        {
            PremiseCode = "Test611111",
            EquipmentCode = "Test765111",
            Quantity = 20
        };

        // Act
        var response = await _client.PostJsonAsync("/Contracts", contract);

        // Assert
        response.Should()
            .HaveErrorMessage("Premise or equipment does not exist")
            .And.Be500InternalServerError();
    }

    [Test]
    public async Task TaskCreateContractAsync_ShouldFailValidationIfPremiseCodeDoesNotContainTenSymbols()
    {
        // Arrange
        var contract = new ContractCreateDto
        {
            PremiseCode = "Test555",
            EquipmentCode = "Test777111",
            Quantity = 1
        };

        // Act
        var response = await _client.PostJsonAsync("/Contracts", contract);

        // Assert
        response.Should()
            .HaveErrorMessage("Premise code should contain only 10 symbols")
            .And.Be400BadRequest();
    }

    [Test]
    public async Task TaskCreateContractAsync_ShouldFailValidationIfEquipmentCodeDoesNotContainTenSymbols()
    {
        // Arrange
        var contract = new ContractCreateDto
        {
            PremiseCode = "Test555111",
            EquipmentCode = "Test777",
            Quantity = 1
        };

        // Act
        var response = await _client.PostJsonAsync("/Contracts", contract);

        // Assert
        response.Should()
            .HaveErrorMessage("Equipment code should contain only 10 symbols")
            .And.Be400BadRequest();
    }

    [Test]
    public async Task TaskCreateContractAsync_ShouldFailValidationIfQuantityLessThanOne()
    {
        // Arrange
        var contract = new ContractCreateDto
        {
            PremiseCode = "Test555111",
            EquipmentCode = "Test777111",
            Quantity = 0
        };

        // Act
        var response = await _client.PostJsonAsync("/Contracts", contract);

        // Assert
        response.Should()
            .HaveErrorMessage("Quanity should be 1 or more")
            .And.Be400BadRequest();
    }
}