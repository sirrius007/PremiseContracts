using FluentValidation;
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

    private static async Task<IResult> GetContractsAsync(
        IPremiseContractsService service)
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
    
    private static async Task<IResult> CreateContractAsync(
        ContractCreateDto contractDto,
        IPremiseContractsService service,
        IValidator<ContractCreateDto> validator)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(contractDto);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }
            await service.CreateContract(contractDto);
            return Results.Created("/Contracts", contractDto);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}
