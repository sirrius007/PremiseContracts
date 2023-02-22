namespace PremiseContractsService.DTOs;

public class ContractDto
{
    public string PremiseName { get; set; } = string.Empty;
    public string EquipmentName { get; set; } = string.Empty;
    public int Quantity { get; set; }
}