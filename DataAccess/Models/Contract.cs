namespace DataAccess.Models
{
    public class Contract
    {
        public int Id { get; set; }
        public string PremiseCode { get; set; } = string.Empty;
        public string EquipmentCode { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public Premise Premise { get; set; } = new Premise();
        public Equipment Equipment { get; set; } = new Equipment();
    }
}
