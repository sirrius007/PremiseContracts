using DataAccess.Data;
using DataAccess.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using PremiseContractsService.DTOs;
using System.Text;

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
            dbContext.Premises.Add(new Premise { Code = "Test7", Name = "Test71", Area = 500 });
            dbContext.Equipment.Add(new Equipment { Code = "Test8", Name = "Test72", Area = 50 });
            dbContext.SaveChanges();
        }

        var contract = new ContractCreateDto
        {
            PremiseCode = "Test7",
            EquipmentCode = "Test8",
            Quantity = 1
        };

        // Act
        var response = await _client.PostAsync("/Contracts", new StringContent(JsonConvert.SerializeObject(contract), Encoding.UTF8, "application/json"));

        // Assert
        response.Should().Be200Ok();
    }

    [Test]
    public async Task TaskCreateContractAsync_ShouldThrowAnExceptionIfAreaIsNotEnough()
    {
        // Arrange
        using (var scope = _factory.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<PremiseContractsContext>();
            dbContext.Premises.Add(new Premise { Code = "Test3", Name = "Test31", Area = 500 });
            dbContext.Equipment.Add(new Equipment { Code = "Test4", Name = "Test42", Area = 50 });
            dbContext.SaveChanges();
        }

        var contract = new ContractCreateDto
        {
            PremiseCode = "Test3",
            EquipmentCode = "Test4",
            Quantity = 20
        };

        // Act
        var response = await _client.PostAsync("/Contracts", new StringContent(JsonConvert.SerializeObject(contract), Encoding.UTF8, "application/json"));

        // Assert
        response.Should()
            .HaveError("Not enough free premise's area for this equipment quantity")
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
            PremiseCode = "Test611",
            EquipmentCode = "Test765",
            Quantity = 20
        };

        // Act
        var response = await _client.PostAsync("/Contracts", new StringContent(JsonConvert.SerializeObject(contract), Encoding.UTF8, "application/json"));

        // Assert
        response.Should()
            .HaveError("Premise or equipment does not exist")
            .And.Be500InternalServerError();
    }
}