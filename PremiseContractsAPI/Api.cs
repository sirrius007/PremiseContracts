using PremiseContractsService.DTOs;
using PremiseContractsService.Interfaces;

namespace PremiseContractsAPI;

public static class Api
{
    public static void ConfigureApi(this WebApplication app)
    {
        app.MapGet("/Contracts", GetContractsAsync);
        app.MapPost("/Contracts", CreateContractAsync);
    }

    private static async Task<IResult> GetContractsAsync(IPremiseContractsService service)
    {
        try
        {
            var result = await service.GetContracts();
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
    
    private static async Task<IResult> CreateContractAsync(ContractCreateDto contractDto, IPremiseContractsService service)
    {
        try
        {
            await service.CreateContract(contractDto);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}
