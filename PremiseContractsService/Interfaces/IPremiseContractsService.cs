using PremiseContractsService.DTOs;

namespace PremiseContractsService.Interfaces;

public interface IPremiseContractsService
{
    Task CreateContract(ContractCreateDto contractDto);
    Task<IEnumerable<ContractDto>> GetContracts();
}