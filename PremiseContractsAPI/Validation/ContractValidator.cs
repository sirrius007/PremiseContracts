using FluentValidation;
using PremiseContractsService.DTOs;

namespace PremiseContractsAPI.Validation
{
    public class ContractValidator : AbstractValidator<ContractCreateDto>
    {
        public ContractValidator()
        {
            RuleFor(p => p.PremiseCode)
                .Length(10)
                .WithName(nameof(ContractCreateDto.PremiseCode))
                .WithMessage("Premise code should contain only 10 symbols");

            RuleFor(p => p.EquipmentCode)
                .Length(10)
                .WithName(nameof(ContractCreateDto.EquipmentCode))
                .WithMessage("Equipment code should contain only 10 symbols");

            RuleFor(p => p.Quantity)
                .GreaterThanOrEqualTo(1)
                .WithName(nameof(ContractCreateDto.Quantity))
                .WithMessage("Quanity should be 1 or more");
        }
    }
}
