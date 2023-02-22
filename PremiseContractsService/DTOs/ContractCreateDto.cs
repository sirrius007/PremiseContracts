namespace PremiseContractsService.DTOs;

public class ContractCreateDto
{
    public string PremiseCode { get; set; } = string.Empty;
    public string EquipmentCode { get; set; } = string.Empty;
    public int Quantity { get; set; }
}