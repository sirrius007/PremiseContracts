using AutoMapper;
using DataAccess.Data.Repositories.Interfaces;
using DataAccess.Models;
using PremiseContractsService.DTOs;
using PremiseContractsService.Interfaces;

namespace PremiseContractsService;

public class PremiseContractsService : IPremiseContractsService
{
    private readonly IContractsRepository _contractRepository;
    private readonly IPremiseRepository _premiseRepository;
    private readonly IEquipmentRepository _equipmentRepository;
    private readonly IMapper _mapper;

    public PremiseContractsService(
        IContractsRepository contractRepository,
        IPremiseRepository premiseRepository,
        IEquipmentRepository equipmentRepository,
        IMapper mapper)
    {
        _contractRepository = contractRepository;
        _premiseRepository = premiseRepository;
        _equipmentRepository = equipmentRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ContractDto>> GetContracts()
    {
        var contracts = await _contractRepository.GetAllAsync();
        var contractsDto = _mapper.Map<IEnumerable<ContractDto>>(contracts);
        return contractsDto;
    }

    public async Task CreateContract(ContractCreateDto contractDto)
    {
        var premise = await _premiseRepository.GetPremise(contractDto.PremiseCode);
        var equipment = await _equipmentRepository.GetEquipment(contractDto.EquipmentCode);
        if (premise is null || equipment is null)
        {
            throw new Exception("Premise or equipment does not exist");
        }
        if (contractDto.Quantity <= 0)
        {
            throw new Exception("Quantity should be 1 or higher");
        }
        var contracts = await _contractRepository.GetAllAsync();
        var usedEquipmentArea = contracts.Where(c => c.PremiseCode == contractDto.PremiseCode).Sum(c => c.Quantity * c.Equipment.Area);
        var premiseAreaTotal = premise.Area;
        var planForEquipmentArea = equipment.Area * contractDto.Quantity;
        if (premiseAreaTotal - usedEquipmentArea - planForEquipmentArea < 0)
        {
            throw new Exception("Not enough free premise's area for this equipment quantity");
        }

        var contract = _mapper.Map<Contract>(contractDto);
        contract.Premise = premise;
        contract.Equipment = equipment;
        await _contractRepository.CreateAsync(contract);
    }

}