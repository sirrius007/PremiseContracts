using AutoMapper;
using DataAccess.Models;
using PremiseContractsService.DTOs;

namespace PremiseContractsService.Mapping;

public class ContractProfile : Profile
{
    public ContractProfile()
    {
        CreateMap<ContractCreateDto, Contract>();
        CreateMap<Contract, ContractDto>()
            .ForMember(dest => dest.PremiseName, src => src.MapFrom(opt => opt.Premise.Name))
            .ForMember(dest => dest.EquipmentName, src => src.MapFrom(opt => opt.Equipment.Name));
    }
}